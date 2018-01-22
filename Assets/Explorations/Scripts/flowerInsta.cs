using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class FlowerInsta : MonoBehaviour {

    List<Mesh> flowers;
    public GameObject instaMesh;
    public int num_flowers = 10;
    public int max_num_flowers = 1000;
    private GameObject instantiatedPrefab;
    public float startSize = 3f;
    private Vector3  baseScale;

    // Use this for initialization
    void Start () {
        SkinnedMesh mesh = GetComponent<SkinnedMesh>();
        mesh.OnResultsReady += NatureBloom;
    }
	

    void NatureBloom(SkinnedMesh mesh) {



        if (mesh.useBakeMesh)
        {
            var m = transform.localToWorldMatrix;
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                Vector3 position = mesh.bakedVertices[i];
                Vector3 normal = mesh.bakedNormals[i];

                Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
                Quaternion w = Quaternion.identity;
                w.SetEulerRotation(Mathf.PI / 2, 0, 0);
                Vector3 worldPt = transform.TransformPoint(position);
                instantiatedPrefab = Instantiate(instaMesh, worldPt, transform.rotation * q * w);
               // baseScale = instantiatedPrefab.transform.localScale;
                //instantiatedPrefab.transform.localScale = baseScale * startSize;

            }
        }
        else
        {
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                Vector3 position = mesh.vertices[i];
                Vector3 normal = mesh.normals[i];
                Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
                Quaternion w = Quaternion.identity;
                w.SetEulerRotation(Mathf.PI / 2, 0, 0);
                Vector3 worldPt = transform.TransformPoint(position);
                instantiatedPrefab = Instantiate(instaMesh, position, transform.rotation * q * w);
                //baseScale = instantiatedPrefab.transform.localScale;
                //instantiatedPrefab.transform.localScale = baseScale * startSize;
            }
        }


    }
}
