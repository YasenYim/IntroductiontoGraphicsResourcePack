using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 矩形 : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    // 用来存放顶点数据
    List<Vector3> verts;        // vertices  顶点
    List<int> indices;          // index的复数，序号
    List<Vector2> uvs;          // uv

    void Start()
    {
        verts = new List<Vector3>();
        indices = new List<int>();
        uvs = new List<Vector2>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        BuildModel();
    }

    void BuildModel()
    {
        verts.Clear();
        indices.Clear();
        uvs.Clear();

        // verts 顶点数组
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(1, 1, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(0, 1, 0));

        // 序号数组
        indices.AddRange(new int[] { 0, 1, 2, 0, 3, 1});

        // uv, 对应顶点
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(10, 10));
        uvs.Add(new Vector2(10, 0));
        uvs.Add(new Vector2(0, 10));

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
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }
}
