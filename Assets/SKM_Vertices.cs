using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKM_Vertices : MonoBehaviour {

    Mesh mesh;
    SkinnedMeshRenderer skin;

    int vertexCount;
    Vector3[] vertices;
    Vector3[] normals;
    public System.Action<SKM_Vertices> OnResultsReady;

    // Use this for initialization
    void Start()
    {

        skin = GetComponent<SkinnedMeshRenderer>();
        mesh = skin.sharedMesh;
        skin.BakeMesh(mesh);
        vertexCount = mesh.vertexCount;
        vertices = new Vector3[vertexCount];
        Debug.Log(vertexCount);
    }

    // Update is called once per frame
    void Update()
    {

        vertices = new Vector3[mesh.vertices.Length]; mesh.vertices.CopyTo(vertices, 0);


    }
}
