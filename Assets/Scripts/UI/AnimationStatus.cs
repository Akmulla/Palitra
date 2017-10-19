using UnityEngine;

public class AnimationStatus : MonoBehaviour
{
   public bool Finished { get; set; }
	
    void Awake()
    {
        Finished = false;
    }

    public void Finish()
    {
        Finished = true;
    }

    public void Begin()
    {
        Finished = false;
    }
}
