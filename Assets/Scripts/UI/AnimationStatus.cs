using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatus : MonoBehaviour
{
    bool finished=true;
	
    public void Finish()
    {
        finished = true;
    }

    public void Begin()
    {
        finished = false;
    }

    public bool GetStatus()
    {
        return finished;
    }
}
