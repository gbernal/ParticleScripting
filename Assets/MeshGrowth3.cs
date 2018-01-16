using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGrowth3 : MonoBehaviour {
    public GameObject instaMesh;
    public int startSize = 3;
    public int minSize = 1;
    public int maxSize = 6;

    public float speed = 2.0f;

    private Vector3 targetScale;
    private Vector3 baseScale;
    private Vector3[] vertices;
    private int currScale;
    private GameObject instantiatedPrefab;
    // Use this for initialization
    void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
       vertices = mesh.vertices;
        int i = 0;
        while (i < vertices.Length)
        {
            Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
            Quaternion w = Quaternion.identity;
            w.SetEulerRotation(Mathf.PI / 2, 0, 0);
            Vector3 worldPt = transform.TransformPoint(vertices[i]);
            instantiatedPrefab = Instantiate(instaMesh, worldPt, transform.rotation * q * w);

            baseScale = instantiatedPrefab.transform.localScale;
            instantiatedPrefab.transform.localScale = baseScale * startSize;
            currScale = startSize;
            targetScale = baseScale * startSize;
            i++;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();


    }
	
	// Update is called once per frame
	void Update () {
       

            instantiatedPrefab.transform.localScale = Vector3.Lerp(instantiatedPrefab.transform.localScale, targetScale, speed * Time.deltaTime);
            Debug.Log(instantiatedPrefab);
            // If you don't want an eased scaling, replace the above line with the following line
            //   and change speed to suit:
            // transform.localScale = Vector3.MoveTowards (transform.localScale, targetScale, speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.UpArrow))
                ChangeSize(true);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                ChangeSize(false);

    }

    public void ChangeSize(bool bigger)
    {

        if (bigger)
            currScale++;
        else
            currScale--;

        currScale = Mathf.Clamp(currScale, minSize, maxSize + 1);

        targetScale = baseScale * currScale;
    }
}
