using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformation : MonoBehaviour
{
    private MeshFilter Mesh;
    private MeshCollider collider;

    private void Start()
    {
        Mesh = GetComponent<MeshFilter>();
        Mesh.mesh.MarkDynamic();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            collider = GetComponent<MeshCollider>();
            var mesh = Mesh.mesh;
            var vertices = mesh.vertices;
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                var p = vertices[i];
                p.y = 0;
                vertices[i] = p;

                mesh.vertices = vertices;
                mesh.RecalculateNormals();
            }
            Destroy(collider);
            gameObject.AddComponent<MeshCollider>();
        }
    }

    public void Deformate(Vector3 point, float radius)
    {
        collider = GetComponent<MeshCollider>();
        var mesh = Mesh.mesh;
        var localBlastPoint = Mesh.transform.InverseTransformPoint(point);
        float r = radius * radius;
        var vertices = mesh.vertices;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 behind = localBlastPoint - mesh.vertices[i];
            float lenght = behind.sqrMagnitude;
            if (r > lenght)
            {
                var p = vertices[i];
                p.y -= 1;
                vertices[i] = p;
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }
        Destroy(collider);
        gameObject.AddComponent<MeshCollider>();
    }
    //private void DeformateTerrain()
    //{
    //    int x = 0;
    //    int y = 0;
    //    float[,] heights = new float[x, y]; // Создаём массив вершин
    //    terrain.terrainData.
    //    // ...
    //    // Делаем с heights всё, что хотим
    //    // ...
    //    terrain.terrainData.SetHeights(0, 0, heights); // И, наконец, применяем нашу карту высот (heights)
    //}
}
