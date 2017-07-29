﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Destroy());
	}
	
	IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}