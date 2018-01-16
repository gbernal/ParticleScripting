using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerInsta : MonoBehaviour {

    List<Mesh> flowers;
    public int num_flowers = 10;
    public int max_num_flowers = 1000;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(flowers == null){
            flowers = new List<Mesh>();
        }
        if (flowers.Count != num_flowers && num_flowers > 0 && num_flowers < max_num_flowers){
            flowers.Clear();

            //add new flowers
            for(int i = 0; i < num_flowers; i++)
            {
                Mesh mesh = Resources.Load("Assets/Rose01.prefab", typeof(Mesh)) as Mesh;
                flowers.Add(mesh);
            }
        }

        Debug.Log("we have " + flowers.Count + " flowers");
    }
}
