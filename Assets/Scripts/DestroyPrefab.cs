using System.Collections;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
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
