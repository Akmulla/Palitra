using UnityEngine;

public class ChngLvlAnim : MonoBehaviour
{
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventManager.StartListening("LvlFinished", LvlFinished);
    }

    void OnDisable()
    {
        EventManager.StopListening("LvlFinished", LvlFinished);
    }

    void LvlFinished()
    {
        _anim.SetTrigger("ChangeLvl");
    }
}
