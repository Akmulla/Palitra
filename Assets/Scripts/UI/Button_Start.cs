﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Start : MonoBehaviour
{
	public void BeginGame()
    {
        if (!Hearts.h.CheckHearts())
            return;

        Hearts.h.Heart--;
        SoundManager.sound_manager.SingleSound(SoundSample.Start);
        GameController.game_controller.BeginGame();

    }
}
