using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGrowthRev1 : MonoBehaviour
{
    public GameObject MeshSpawn;
    public float scaleRate = 1.0f;
    public float minScale = 775.0f;
    public float maxScale = 1.0f;
    // Use this for initialization
    void Start()
    {


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        int i = 0;
        while (i < vertices.Length)
        {

            //Instantiate(instaMesh, transform.position, transform.rotation);
            Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
            Quaternion w = Quaternion.identity;
            w.SetEulerRotation(Mathf.PI / 2, 0, 0);
            Vector3 worldPt = transform.TransformPoint(vertices[i]);
            var instantiatedPrefab = Instantiate(MeshSpawn, worldPt, transform.rotation * q * w) as GameObject;

            Debug.Log(instantiatedPrefab.transform.localScale.x);
            instantiatedPrefab.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            i++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

    }
}
