using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    Animator _anim;
    
    void Awake()
    {
        _anim=GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventManager.StartListening("BeginGameAnimation", Animate);
        EventManager.StartListening("ResetGameAnimation", Reset);
    }

    void OnDisable()
    {
        EventManager.StopListening("BeginGameAnimation", Animate);
        EventManager.StopListening("ResetGameAnimation", Reset);
    }

    void Animate()
    {
        _anim.SetBool("animate", true);
    }

    void Reset()
    {
        _anim.SetBool("animate", false);
    }
}
