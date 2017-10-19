using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screamer : MonoBehaviour
{
    float chance=1.0f;
    public Sprite[] pics;
    public Image screamer_img;

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
        if (Ball.ball.trian_type != TrianType.Screamer)
            return;

        float roll = Random.value;

        if (roll<=chance)
        {
           StartCoroutine(ShowScreamer());
        }
        else
        {
            chance += 0.1f;
        }
    }

    IEnumerator ShowScreamer()
    {
        SoundManager.sound_manager.Screamer();
        screamer_img.sprite = pics[Random.Range(0, pics.Length)];
        screamer_img.enabled = true;

        //GameController.game_controller.Pause();
        yield return new WaitForSeconds(1.0f);
        screamer_img.enabled = false;
    }
}
