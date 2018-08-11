using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatus : MonoBehaviour
{
   public bool finished { get; set; }
	
    void Awake()
    {
        finished = false;
    }

    public void Finish()
    {
        finished = true;
    }

    public void Begin()
    {
        finished = false;
    }
}
