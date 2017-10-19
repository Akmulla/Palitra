using UnityEngine;

public class SaveAnimState : MonoBehaviour
{
    [SerializeField]
    Animator _anim;

	void OnEnable()
    {
        if (GameController.gameController.GetState()==GameState.Game)
        {
            _anim.SetBool("finished", true);
        }
        else
        {
            _anim.SetBool("finished", false);
        }
    }
}
