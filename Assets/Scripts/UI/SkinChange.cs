using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{
    public Image[] sectors;
    public Text skin_count_text;
    public Text price_text;
    public Text score_text;
    //public Text set_skin_text;
    public GameObject buy_button;
    public GameObject set_button;
    public Image BG;
    public Animator money_anim;
    int skin_number;

    public void NextSkin()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ScrollSkin);
        if (skin_number == SkinManager.skin_manager.GetTotalSkinCount() - 1)
        {
            skin_number = 0;
        }
        else
        {
            skin_number++;
        }
        SkinManager.skin_manager.SetActiveSkin(skin_number);
        UpdateSkin();
        UpdateText();
    }

    public void PrevSkin()
    {
        SoundManager.sound_manager.SingleSound(SoundSample.ScrollSkin);
        if (skin_number == 0)
        {
            skin_number = SkinManager.skin_manager.GetTotalSkinCount() - 1;
        }
        else
        {
            skin_number--;
        }
        SkinManager.skin_manager.SetActiveSkin(skin_number);
        UpdateSkin();
        UpdateText();
    }

    public void SetSkin()
    {
        if (CheckIfAvailable(skin_number))
        {
            SoundManager.sound_manager.SingleSound(SoundSample.SetSkin);
            SkinManager.skin_manager.SetActiveSkin(skin_number);
            SkinManager.skin_manager.SaveActiveSkin();
        }
        else
        {
            if (GlobalScore.global_score.Score >= SkinManager.skin_manager.GetSkinByNumber(skin_number).price)
            {
                SoundManager.sound_manager.SingleSound(SoundSample.Buy);
                GlobalScore.global_score.Score -= SkinManager.skin_manager.GetSkinByNumber(skin_number).price;
                PlayerPrefs.SetInt(SkinManager.skin_manager.GetSkinByNumber(skin_number).name, 1);
            }
            else
            {
                SoundManager.sound_manager.SingleSound(SoundSample.Error);
                money_anim.SetTrigger("animate");
            }
        }
        
        UpdateText();
    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        //print("fg");
        InitMenu();
    }

    public void InitMenu()
    {
        skin_number = SkinManager.skin_manager.GetSkinNumber();
        SkinManager.skin_manager.SetActiveSkin(skin_number);
        for (int i = 0; i < SkinManager.skin_manager.GetTotalSkinCount(); i++)
        {
            if ((!CheckIfAvailable(i)) && (SkinManager.skin_manager.GetSkinByNumber(i).price == 0))
            {
                PlayerPrefs.SetInt(SkinManager.skin_manager.GetSkinByNumber(i).name, 1);
            }
        }
        UpdateSkin();
        UpdateText();
        //UpdateScore();

    }

    void UpdateSkin()
    {
        for (int i = 0; i < sectors.Length;i++)
        {
            sectors[i].color = SkinManager.skin_manager.GetSkinByNumber(skin_number).colors[i];
        }
        BG.color = SkinManager.skin_manager.GetSkinByNumber(skin_number).bg_color;
        //EventManager.TriggerEvent("SkinChanged");
    }

    void UpdateText()
    {
        skin_count_text.text = (skin_number+1).ToString()+"/"+SkinManager.skin_manager.GetTotalSkinCount();
        price_text.text = SkinManager.skin_manager.GetSkinByNumber(skin_number).price.ToString();
        score_text.text = GlobalScore.global_score.Score.ToString();
        if (CheckIfAvailable(skin_number))
        {
            //set_skin_text.text = "Set";
            set_button.SetActive(true);
            buy_button.SetActive(false);
        }
        else
        {
            //set_skin_text.text = "Buy";
            set_button.SetActive(false);
            buy_button.SetActive(true);
        }
    }

    //void UpdateScore()
    //{
    //    score_text.text = GlobalScore.global_score.Score.ToString();
    //}
	
    bool CheckIfAvailable(int number)
    {
        if (PlayerPrefs.HasKey(SkinManager.skin_manager.GetSkinByNumber(number).name))
        {
            return true;
        }
        
        return false;
    }
}
