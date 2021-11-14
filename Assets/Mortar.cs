using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public Camera CastingCamera;
    public LineRenderer TrajectoryRenderer;
    private Transform _transform;
    public GameObject Projectile;
    public AnimationCurve Trajectory;
    public const int SegmentCount = 25;
    public float Radius = 3;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        TrajectoryRenderer.positionCount = SegmentCount + 1;
    }
    private void Update()
    {
        Ray ray = CastingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            Vector3[] points = new Vector3[SegmentCount + 2];

            Vector3 step = (hit.point - _transform.position) / SegmentCount;
            points[0] = _transform.position;
            TrajectoryRenderer.SetPosition(0, transform.position);
            for(int i = 0; i < SegmentCount; i++)
            {
                Vector3 positon = _transform.position + (step * i);
                positon.y = Trajectory.Evaluate(i / (float) SegmentCount);
                points[i + 1] = positon;
            }
            points[SegmentCount + 1] = hit.point;

            TrajectoryRenderer.SetPositions(points);

            if(Input.GetMouseButtonDown(0))
            {
                GameObject projectileGo = Instantiate(Projectile);
                projectileGo.GetComponent<ProjectTile>().Shoot(points);

                projectileGo.GetComponent<ProjectTile>().OnEnd += blastpoint => hit.transform.GetComponent<MeshDeformation>().Deformate(blastpoint, Radius);
            }
        }
    }
}
