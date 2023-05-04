using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 复用顶点立方体 : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    // 用来存放顶点数据
    List<Vector3> verts;        // vertices  顶点
    List<int> indices;          // index的复数，序号

    void Start()
    {
        verts = new List<Vector3>();
        indices = new List<int>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        BuildModel();
    }

    void BuildModel()
    {
        verts.Clear();
        indices.Clear();

        // verts 顶点数组
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 0, 1));
        verts.Add(new Vector3(0, 0, 1));
        verts.Add(new Vector3(0, 1, 0));
        verts.Add(new Vector3(1, 1, 0));
        verts.Add(new Vector3(1, 1, 1));
        verts.Add(new Vector3(0, 1, 1));

        // 序号数组
        indices.AddRange(new int[] { 0,1,2,0,2,3,
            3,4,0,3,7,4,
            0,5,1,0,4,5,
            1,5,2,2,5,6,
            4,7,5,5,7,6,
            3,2,6,3,6,7});

        Draw();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildModel();
        }
    }

    public void Draw()
    {
        // 套路
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }
}
