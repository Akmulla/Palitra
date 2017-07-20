using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    Transform cam;
    float offset;
    Transform tran;

	void Awake ()
    {
        cam = Camera.main.gameObject.transform;
        tran = GetComponent<Transform>();
        offset = tran.position.y - cam.position.y;
	}
	
	
	void Update ()
    {
        tran.position = new Vector3(cam.position.x, cam.position.y+offset,1.0f);
	}
}
