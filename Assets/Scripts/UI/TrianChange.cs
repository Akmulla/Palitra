using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianChange : MonoBehaviour
{

    void UpdateTrianSkin()
    {
        //SetEquipButtonStatus();
        //for (int i = 0; i < sectors.Length; i++)
        //{
        //    sectors[i].color = SkinManager.skin_manager.GetSkinByNumber(skin_number).colors[i];
        //}
        //BG.color = SkinManager.skin_manager.GetSkinByNumber(skin_number).bg_color;
    }

    void UpdateText()
    {
        skin_count_text.text = (skin_number + 1).ToString() + "/" + SkinManager.skin_manager.GetTotalSkinCount();
        price_text.text = SkinManager.skin_manager.GetSkinByNumber(skin_number).price.ToString();
        score_text.text = GlobalScore.global_score.Score.ToString();
        if (CheckIfAvailable(skin_number))
        {
            set_button.SetActive(true);
            buy_button.SetActive(false);
        }
        else
        {
            set_button.SetActive(false);
            buy_button.SetActive(true);
        }
    }

    bool CheckIfAvailable(int number)
    {
        if (PlayerPrefs.HasKey(SkinManager.skin_manager.GetSkinByNumber(number).name))
        {
            return true;
        }

        return false;
    }

    void SetEquipButtonStatus()
    {
        if (skin_number == PlayerPrefs.GetInt("SavedSkin"))
        {
            buy_set_button.interactable = false;
        }
        else
        {
            buy_set_button.interactable = true;
        }
    }

    public void NextTrian()
    {

    }

    public void PrevTrian()
    {

    }
}
