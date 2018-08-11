using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHeartMenu : MonoBehaviour
{
    public int hearts;
    public int price;

	public void BuyHeart()
    {
        if (GlobalScore.global_score.Score >= price)
        {
            GlobalScore.global_score.Score -= price;
            Hearts.h.Heart += hearts;
        }
        else
        {

            UIController.ui.OpenRubinMenu();
            UIController.ui.CloseLifeMenu();
        }
    }
}
