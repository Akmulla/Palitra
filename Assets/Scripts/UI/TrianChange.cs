using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrianChange : MonoBehaviour
{
    public Image trian;
    int trian_number = 0;
    public Button buy_set_button;
    public Animator money_anim;
    public GameObject buy_button;
    public GameObject set_button;
    public Text trian_count_text;
    public Text price_text;
    public Text score_text;
    public Text description;

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
        trian_count_text.text = (trian_number + 1).ToString() + "/" + TrianManager.trian_manager.GetTotalTrianCount();
        price_text.text = TrianManager.trian_manager.GetTrianByNumber(trian_number).price.ToString();
        score_text.text = GlobalScore.global_score.Score.ToString();
        description.text = TrianManager.trian_manager.GetTrianByNumber(trian_number).description.ToString();
        if (CheckIfAvailable(trian_number))
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

    void OnEnable()
    {
        InitMenu();
    }

    void InitMenu()
    {
        trian_number = TrianManager.trian_manager.GetTrianNumber();
        TrianManager.trian_manager.SetActiveTrian(trian_number);
        for (int i = 0; i < TrianManager.trian_manager.GetTotalTrianCount(); i++)
        {
            if ((!CheckIfAvailable(i)) && (TrianManager.trian_manager.GetTrianByNumber(i).price == 0))
            {
                PlayerPrefs.SetInt(TrianManager.trian_manager.GetTrianByNumber(i).name, 1);
                PlayerPrefs.Save();
            }

        }
        UpdateTrian();
        UpdateText();
    }

    void Awake()
    {
        //trian_number = TrianManager.trian_manager.GetTrianNumber();
        InitMenu();
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
        //UpdateTrian();
        //UpdateText();
    }

    bool CheckIfAvailable(int number)
    {
        if (PlayerPrefs.HasKey(TrianManager.trian_manager.GetTrianByNumber(number).name))
        {
            return true;
        }

        return false;
    }

    void SetEquipButtonStatus()
    {
        if (trian_number == PlayerPrefs.GetInt("SavedTrian"))
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
        SoundManager.sound_manager.SingleSound(SoundSample.ScrollSkin);
        if (trian_number == 0)
        {
            trian_number = TrianManager.trian_manager.GetTotalTrianCount() - 1;
        }
        else
        {
            trian_number--;
        }
        //SkinManager.skin_manager.SetActiveSkin(trian_number);
        UpdateTrian();
        UpdateText();
    }

    public void SetTrian()
    {
        if (CheckIfAvailable(trian_number))
        {
            SoundManager.sound_manager.SingleSound(SoundSample.SetSkin);
            TrianManager.trian_manager.SetActiveTrian(trian_number);
            TrianManager.trian_manager.SaveActiveTrian();
            //buy_set_button.interactable = false;
            //color_buy.Animate();
        }
        else
        {
            if (GlobalScore.global_score.Score >= TrianManager.trian_manager.GetTrianByNumber(trian_number).price)
            {
                SoundManager.sound_manager.SingleSound(SoundSample.Buy);
                GlobalScore.global_score.Score -= TrianManager.trian_manager.GetTrianByNumber(trian_number).price;
                PlayerPrefs.SetInt(TrianManager.trian_manager.GetTrianByNumber(trian_number).name, 1);

                //сразу активируем
                TrianManager.trian_manager.SetActiveTrian(trian_number);
                TrianManager.trian_manager.SaveActiveTrian();

                //color_buy.Animate();
            }
            else
            {
                SoundManager.sound_manager.SingleSound(SoundSample.Error);
                money_anim.SetTrigger("animate");
            }
        }
        SetEquipButtonStatus();
        UpdateText();
    }
}
