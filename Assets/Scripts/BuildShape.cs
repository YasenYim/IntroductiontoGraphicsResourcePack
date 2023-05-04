using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
    Triangle,
    Cube1,
    Cube2,
    BigCube,
    Sphere,
    Circle,
}

public class BuildShape : MonoBehaviour
{
    public Shape shape;
    public int N = 20;
    public float width = 0.1f;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    // 用来存放顶点数据
    List<Vector3> verts;
    List<int> indices;
    List<Vector2> uvs;

    void Start()
    {
        verts = new List<Vector3>();
        indices = new List<int>();
        uvs = new List<Vector2>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        BuildModel();
    }

    void BuildModel()
    {
        ClearMeshData();
        switch (shape)
        {
            case Shape.Triangle:
                AddMeshData_Simple();
                break;
            case Shape.Cube1:
                AddMeshData_Cube1();
                break;
            case Shape.Cube2:
                AddMeshData_Cube2();
                break;
            case Shape.BigCube:
                AddMeshData_BigCube();
                break;
            case Shape.Sphere:
                AddMeshData_Sphere();
                break;
            case Shape.Circle:
                AddMeshData_Circle();
                break;
        }
        Draw();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildModel();
        }
    }

    void ClearMeshData()
    {
        verts.Clear();
        indices.Clear();
        uvs.Clear();
    }

    void AddMeshData_Simple()
    {
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 1, 0));

        indices.AddRange(new int[] { 0, 2, 1 });

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(1, 1));
    }

    void AddMeshData_Circle()
    {
        float d = 2 * Mathf.PI / N;
        float R = 5;
        Vector3 o = Vector3.zero;
        verts.Add(o);
        for (float th=0; th<2*Mathf.PI; th+=d)
        {
            float x = R * Mathf.Sin(th);
            float z = R * Mathf.Cos(th);

            Vector3 p = new Vector3(x, 0, z);

            verts.Add(p);
        }

        for (int i=0; i<verts.Count-2; i++)
        {
            indices.Add(0);
            indices.Add(i + 1);
            indices.Add(i + 2);
        }
        indices.Add(0);
        indices.Add(verts.Count-1);
        indices.Add(1);
    }

    void AddMeshData_Cube1()
    {
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 0, 1));
        verts.Add(new Vector3(0, 0, 1));

        verts.Add(new Vector3(0, 1, 0));
        verts.Add(new Vector3(1, 1, 0));
        verts.Add(new Vector3(1, 1, 1));
        verts.Add(new Vector3(0, 1, 1));

        indices.Add(0); indices.Add(1); indices.Add(2);
        indices.Add(0); indices.Add(2); indices.Add(3);

        indices.Add(4); indices.Add(6); indices.Add(5);
        indices.Add(4); indices.Add(7); indices.Add(6);

        indices.Add(0); indices.Add(4); indices.Add(5);
        indices.Add(0); indices.Add(5); indices.Add(1);

        indices.Add(1); indices.Add(5); indices.Add(6);
        indices.Add(1); indices.Add(6); indices.Add(2);

        indices.Add(0); indices.Add(3); indices.Add(7);
        indices.Add(0); indices.Add(7); indices.Add(4);

        indices.Add(3); indices.Add(2); indices.Add(6);
        indices.Add(3); indices.Add(6); indices.Add(7);
    }

    void AddMeshData_Cube2()
    {
        // 上
        verts.Add(new Vector3(0, 1, 0));
        verts.Add(new Vector3(0, 1, 1));
        verts.Add(new Vector3(1, 1, 1));
        verts.Add(new Vector3(1, 1, 0));
        indices.Add(0); indices.Add(1); indices.Add(2);
        indices.Add(0); indices.Add(2); indices.Add(3);

        float du = 1f / 8;
        float dv = 1f / 16;

        uvs.Add(new Vector2(du*5, dv*8));
        uvs.Add(new Vector2(du*5, dv*9));
        uvs.Add(new Vector2(du*6, dv*9));
        uvs.Add(new Vector2(du*6, dv*8));

        // 右
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 0, 1));
        verts.Add(new Vector3(1, 1, 1));
        verts.Add(new Vector3(1, 1, 0));
        indices.Add(4); indices.Add(6); indices.Add(5);
        indices.Add(4); indices.Add(7); indices.Add(6);

        uvs.Add(new Vector2(0, dv*8));
        uvs.Add(new Vector2(du, dv*8));
        uvs.Add(new Vector2(du, dv*9));
        uvs.Add(new Vector2(0, dv*9));

        // 前
        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(1, 0, 0));
        verts.Add(new Vector3(1, 1, 0));
        verts.Add(new Vector3(0, 1, 0));
        indices.Add(8); indices.Add(10); indices.Add(9);
        indices.Add(8); indices.Add(11); indices.Add(10);

        uvs.Add(new Vector2(0, dv*8));
        uvs.Add(new Vector2(du, dv*8));
        uvs.Add(new Vector2(du, dv*9));
        uvs.Add(new Vector2(0, dv*9));

    }

    void AddMeshData_BigCube()
    {
        int len = 0;
        // 上面
        for (int z = 0; z < N; z++)
        {
            for (int x = 0; x < N; x++)
            {
                float y = (N-1) * width;
                verts.Add(new Vector3(x * width, y, z * width));
            }
        }

        for (int k = 0; k < N - 1; k++)
        {
            for (int j = 0; j < N - 1; j++)
            {
                indices.Add(k * N + j);
                indices.Add((k + 1) * N + j);
                indices.Add(k * N + j + 1);

                indices.Add((k + 1) * N + j + 1);
                indices.Add(k * N + j + 1);
                indices.Add((k + 1) * N + j);
            }
        }
        len = verts.Count;

        // 前面
        for (int y = 0; y < N; y++)
        {
            for (int x = 0; x < N; x++)
            {
                float z = 0;
                verts.Add(new Vector3(x * width, y * width, z));
            }
        }
        
        for (int k = 0; k < N - 1; k++)
        {
            for (int j = 0; j < N - 1; j++)
            {
                indices.Add(len+k * N + j);
                indices.Add(len+(k + 1) * N + j);
                indices.Add(len+k * N + j + 1);

                indices.Add(len+(k + 1) * N + j + 1);
                indices.Add(len+k * N + j + 1);
                indices.Add(len+(k + 1) * N + j);
            }
        }
        len = verts.Count;

        // 右面
        for (int y = 0; y < N; y++)
        {
            for (int z = 0; z < N; z++)
            {
                float x = (N-1) * width;
                verts.Add(new Vector3(x, y * width, z * width));
            }
        }

        for (int k = 0; k < N - 1; k++)
        {
            for (int j = 0; j < N - 1; j++)
            {
                indices.Add(len + k * N + j);
                indices.Add(len + (k + 1) * N + j);
                indices.Add(len + k * N + j + 1);

                indices.Add(len + (k + 1) * N + j + 1);
                indices.Add(len + k * N + j + 1);
                indices.Add(len + (k + 1) * N + j);
            }
        }
        len = verts.Count;
    }

    void AddMeshData_Sphere()
    {
        AddMeshData_BigCube();

        Vector3 o = Vector3.one * (N - 1) * width / 2.0f;

        for (int i=0; i<verts.Count; i++)
        {
            Vector3 v = verts[i] - o;
            v = v.normalized * (N * width);
            verts[i] = v;
        }
    }

    public void Draw()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Vector3[] normals = mesh.normals;
        // 修改normals
        mesh.normals = normals;

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

}
