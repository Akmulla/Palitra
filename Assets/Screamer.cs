using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Screamer : MonoBehaviour
{
    float _chance=1.0f;
    public Sprite[] pics;
    public Image screamerImg;

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
        if (Ball.ball.trianType != TrianType.Screamer)
            return;

        float roll = Random.value;

        if (roll<=_chance)
        {
           StartCoroutine(ShowScreamer());
        }
        else
        {
            _chance += 0.1f;
        }
    }

    IEnumerator ShowScreamer()
    {
        SoundManager.soundManager.Screamer();
        screamerImg.sprite = pics[Random.Range(0, pics.Length)];
        screamerImg.enabled = true;

        //GameController.game_controller.Pause();
        yield return new WaitForSeconds(1.0f);
        screamerImg.enabled = false;
    }
}
