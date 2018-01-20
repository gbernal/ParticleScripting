using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGrowth1 : MonoBehaviour
{
    public GameObject instaMesh;
    //private GameObject instaMesh[];

    public float startSize = 3f;
    public float minSize = 1f;
    public float maxSize = 2f;

    public float speed = 2.0f;

    private Vector3 targetScale;
    private Vector3 baseScale;
    private float currScale;
    private GameObject instantiatedPrefab;
    void Start()
    {
        //instaMesh = new Mesh()[numberTypes];



        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int i = 0;
        #region        
        /*

        while (i < vertices.Length)
                {
                    Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
                    Quaternion w = Quaternion.identity;
                    w.SetEulerRotation(Mathf.PI / 2, 0, 0);
                    Vector3 worldPt = transform.TransformPoint(vertices[i]);
                    instantiatedPrefab = Instantiate(instaMesh, worldPt, transform.rotation * q * w) ;
                    //int i = Random.Range(0, 2);
                    //instantiatedPrefab = Instantiate(instaMesh[i]

                    baseScale = instantiatedPrefab.transform.localScale;
                    instantiatedPrefab.transform.localScale = baseScale * startSize;
                    currScale = startSize;
                    targetScale = baseScale * startSize;
                    i++;
                }
                 
        */
        #endregion
        while (i < vertices.Length)
        {
            bool myBool = (Random.value < 0.5f);
            if (myBool)
            {
                float numRandom = (Random.value * maxSize) + 0.5f;
                Quaternion q = Quaternion.LookRotation(mesh.normals[i], Vector3.up);
                Quaternion w = Quaternion.identity;
                w.SetEulerRotation(Mathf.PI / 2, 0, 0);
                Vector3 worldPt = transform.TransformPoint(vertices[i]);
                instantiatedPrefab = Instantiate(instaMesh, worldPt, transform.rotation * q * w);
                //int i = Random.Range(0, 2);
                //instantiatedPrefab = Instantiate(instaMesh[i]
                baseScale = instantiatedPrefab.transform.localScale;
                instantiatedPrefab.transform.localScale = baseScale * startSize;
                //instantiatedPrefab.transform.localScale = Vector3.one * numRandom;
                currScale = startSize;
                targetScale = baseScale * startSize;
            }
            i++;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    void Update()
    {
        //GameObject tmp[] = 

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
    public void MakeNewObject() {

        float numRandom = (Random.value * maxSize) + 0.5f;
        GameObject objectMade = Instantiate(instaMesh, transform.position, transform.rotation);
        objectMade.transform.localScale = Vector3.one * numRandom;
    }
}

