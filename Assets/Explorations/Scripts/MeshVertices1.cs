using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVertices1 : MonoBehaviour {


    public int startSize = 3;
    public int minSize = 1;
    public int maxSize = 6;

    public float speed = 2.0f;

    private Vector3 targetScale;
    private Vector3 baseScale;
    private int currScale;
    public Mesh mesh;
    public GameObject instaMesh;
    public Vector3[] vertices;
     void Start()
    {

        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        baseScale = transform.localScale;
        transform.localScale = baseScale * startSize;
        currScale = startSize;
        targetScale = baseScale * startSize;
        
    }

    void Update()
    {
        int i = 0;
        while (i < vertices.Length)
        {

            //Instantiate(instaMesh, transform.position, transform.rotation);
            Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
            Quaternion w = Quaternion.identity;
            w.SetEulerRotation(Mathf.PI / 2, 0, 0);
            Vector3 worldPt = transform.TransformPoint(vertices[i]);
            var instantiatedPrefab = Instantiate(instaMesh, worldPt, transform.rotation * q * w) as GameObject;

            while (instantiatedPrefab.transform.localScale.x < 10.0)

            {
                Debug.Log(instantiatedPrefab.transform.localScale.x);

                instantiatedPrefab.transform.localScale = Vector3.Lerp(instantiatedPrefab.transform.localScale, targetScale, speed * Time.deltaTime);

                // If you don't want an eased scaling, replace the above line with the following line
                //   and change speed to suit:
                // transform.localScale = Vector3.MoveTowards (transform.localScale, targetScale, speed * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ChangeSize(true);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    ChangeSize(false);

            }



            i++;
        }
        // mesh.vertices = vertices;
        mesh.RecalculateBounds();
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
