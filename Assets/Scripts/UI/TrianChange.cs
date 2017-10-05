using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrianChange : MonoBehaviour
{
    public Image trian;
    int trian_number = 0;

    void UpdateTrian()
    {
        SetEquipButtonStatus();
        trian.sprite = TrianManager.trian_manager.GetTrianByNumber(trian_number).sprite;

        //for (int i = 0; i < sectors.Length; i++)
        //{
        //    sectors[i].color = SkinManager.skin_manager.GetSkinByNumber(skin_number).colors[i];
        //}
        //BG.color = SkinManager.skin_manager.GetSkinByNumber(skin_number).bg_color;
    }

    void UpdateText()
    {
        //skin_count_text.text = (skin_number + 1).ToString() + "/" + SkinManager.skin_manager.GetTotalSkinCount();
        //price_text.text = SkinManager.skin_manager.GetSkinByNumber(skin_number).price.ToString();
        //score_text.text = GlobalScore.global_score.Score.ToString();
        //if (CheckIfAvailable(skin_number))
        //{
        //    set_button.SetActive(true);
        //    buy_button.SetActive(false);
        //}
        //else
        //{
        //    set_button.SetActive(false);
        //    buy_button.SetActive(true);
        //}
    }

    void OnEnable()
    {
        InitMenu();
    }

    void InitMenu()
    {
        //skin_number = SkinManager.skin_manager.GetSkinNumber();
        //SkinManager.skin_manager.SetActiveSkin(skin_number);
        //for (int i = 0; i < SkinManager.skin_manager.GetTotalSkinCount(); i++)
        //{
        //    if ((!CheckIfAvailable(i)) && (SkinManager.skin_manager.GetSkinByNumber(i).price == 0))
        //    {
        //        PlayerPrefs.SetInt(SkinManager.skin_manager.GetSkinByNumber(i).name, 1);
        //        PlayerPrefs.Save();
        //    }

        //}
        //UpdateSkin();
        //UpdateText();
    }

    void Awake()
    {
        //skin_number = SkinManager.skin_manager.GetSkinNumber();
        //SkinManager.skin_manager.SetActiveSkin(skin_number);
        //for (int i = 0; i < SkinManager.skin_manager.GetTotalSkinCount(); i++)
        //{
        //    if ((!CheckIfAvailable(i)) && (SkinManager.skin_manager.GetSkinByNumber(i).price == 0))
        //    {
        //        PlayerPrefs.SetInt(SkinManager.skin_manager.GetSkinByNumber(i).name, 1);
        //        PlayerPrefs.Save();
        //    }

        //}
        //UpdateSkin();
        //UpdateText();
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
        //if (skin_number == PlayerPrefs.GetInt("SavedSkin"))
        //{
        //    buy_set_button.interactable = false;
        //}
        //else
        //{
        //    buy_set_button.interactable = true;
        //}
    }

    public void NextTrian()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ScrollSkin);
        if (trian_number == TrianManager.trian_manager.GetTotalTrianCount() - 1)
        {
            trian_number = 0;
        }
        else
        {
            trian_number++;
        }
        //SkinManager.skin_manager.SetActiveSkin(trian_number);
        UpdateTrian();
        UpdateText();
    }

    public void PrevTrian()
    {

    }
}
