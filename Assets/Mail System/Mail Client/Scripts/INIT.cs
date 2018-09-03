using UnityEngine;
using System.Collections;

public class INIT : MonoBehaviour {
	public GameObject sysPrefab;
	// Use this for initialization
	void Start () {
	GameObject syst = Instantiate(sysPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		syst.name = "System";		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
