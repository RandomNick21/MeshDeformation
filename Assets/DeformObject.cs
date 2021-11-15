using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeformObject : MonoBehaviour
{
    public SnowBrush snow;
    public UnityAction<Vector3> OnEnd;
    private Vector3[] _trajectory;
    private Transform _transform;
    private int _count;
    public float Speed;
    private void OnCollisionEnter(Collision collision)
    {
        snow = collision.gameObject.GetComponent<SnowBrush>();
        snow?.DrawWithTires(gameObject);
        
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    public void Shoot(Vector3[] trajectory)
    {
        _transform = GetComponent<Transform>();
        _transform.position = trajectory[0];
        _trajectory = trajectory;
        Debug.Log("aa");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _trajectory[_count], Speed * Time.deltaTime);
        if (_transform.position == _trajectory[_count])
        {
            Vector3 lastPoint = _trajectory[_count];
            _count++;
            if (_count >= _trajectory.Length)
            {
                OnEnd?.Invoke(lastPoint);
                Destroy(gameObject);
            }
        }
    }
}
