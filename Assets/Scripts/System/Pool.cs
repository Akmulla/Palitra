using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

public class Pool : MonoBehaviour
{
    public PoolType pool_type;
    protected GameObject[] stck;
   // private int tos;
    public GameObject obj;
    public int size;

    public virtual void Awake() 
    {
        StartCoroutine(AwakeCor());
    }

    IEnumerator AwakeCor()
    {
        stck = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            stck[i] = ((GameObject)Instantiate(obj, Vector2.zero, Quaternion.identity));
            stck[i].GetComponent<PoolRef>().SetPool(this);
            stck[i].SetActive(false);
            yield return null;
        }
    }

    public virtual IEnumerator InitLineCor(int k)
    {
        while (stck[k].activeSelf)
            yield return new WaitForEndOfFrame();
        stck[k].GetComponent<Line>().InitLine();
        yield return null;
    }

    public GameObject Activate(Vector3 pos, Quaternion rot)
    {
        GameObject obj = Pop();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        return (obj);
    }

    public void Deactivate(GameObject obj)
    {
        obj.SetActive(false);
    }

    public virtual GameObject Pop()
    {
        List<GameObject> avail=new List<GameObject>();
        for (int i=0;i<stck.Length;i++)
        {
                if (!stck[i].activeSelf)
                    avail.Add(stck[i]);
            
        }
        return avail[Random.Range(0, avail.Count)];
    }
}
