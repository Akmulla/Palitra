using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAnimState : MonoBehaviour
{
    [SerializeField]
    Animator anim;

	void OnEnable()
    {
        if (GameController.game_controller.GetState()==GameState.Game)
        {
            anim.SetBool("finished", true);
        }
        else
        {
            anim.SetBool("finished", false);
        }
    }
}
