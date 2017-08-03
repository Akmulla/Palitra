using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMeshes : MonoBehaviour
{
    public GameObject[] mesh;
    public MeshData mesh_data;
	// Use this for initialization
	void Start ()
    {
		for (int i=0;i<mesh.Length;i++)
        {
            mesh_data.meshes[i]= mesh[i].transform.GetChild(0).GetChild(0).
                gameObject.GetComponent<MeshFilter>().sharedMesh;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
