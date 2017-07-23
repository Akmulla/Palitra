using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

public class Pool : MonoBehaviour
{
    public PoolType pool_type;
    private GameObject[] stck;
   // private int tos;
    public GameObject obj;
    public int size;

    public void Awake() 
    {
        //stck = new GameObject[size];
        //tos = size;
        //for (int i = 0 ; i < size ; i++)
        //{
        //    stck[i] = ((GameObject)Instantiate(obj, Vector2.zero, Quaternion.identity));
        //    stck[i].GetComponent<PoolRef>().SetPool(this);
        //    stck[i].SetActive(false);
        //}

        StartCoroutine(AwakeCor());
    }
    IEnumerator AwakeCor()
    {
        stck = new GameObject[size];
        //tos = size;
        for (int i = 0; i < size; i++)
        {
            stck[i] = ((GameObject)Instantiate(obj, Vector2.zero, Quaternion.identity));
            stck[i].GetComponent<PoolRef>().SetPool(this);
            stck[i].SetActive(false);
            yield return null;
        }
    }

    

    public IEnumerator InitLineCor(int k)
    {
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

    //public void Push(GameObject obj, int ind)
    //{
    //    stck[ind] = obj;
    //}

    public GameObject Pop()
    {
        List<GameObject> avail=new List<GameObject>();
        for (int i=0;i<stck.Length;i++)
        {
            if (!stck[i].activeSelf)
                avail.Add(stck[i]);
        }

        return avail[Random.Range(0, avail.Count)];
    }
    //public void Deactivate(GameObject obj)
    //{
    //    obj.SetActive(false);
    //    Push(obj);
    //}

    //public void Push(GameObject obj)
    //{
    //    if (tos >= stck.Length)
    //    {
    //        Debug.Log("Стек заполнен");
    //        return;
    //    }
    //    stck[tos] = obj;
    //    tos++;
    //}

    //public GameObject Pop()
    //{
    //    if (tos <= 0)
    //    {
    //        Debug.Log("Стек пуст");
    //        return null;
    //    }
    //    tos--;
    //    return stck[tos];
    //}

    //public void InitLines()
    //{
    //    for (int i = 0; i < size; i++)
    //    {
    //        stck[i].GetComponent<Line>().InitLine();
    //    }
    //}



    //public void InitLine(int k)
    //{
    //    stck[k].GetComponent<Line>().InitLine();
    //}
}
