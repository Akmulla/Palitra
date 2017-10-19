using UnityEngine;
using UnityEngine.UI;

public class SkinChange : MonoBehaviour
{
    public Image[] sectors;
    public Text skinCountText;
    public Text priceText;
    public Text scoreText;
    //public Text set_skin_text;
    public GameObject buyButton;
    public GameObject setButton;
    //public Image BG;
    public Animator moneyAnim;

    public Button buySetButton;

    public ColorBuyAnim colorBuy;

    int _skinNumber;

    public void NextSkin()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ScrollSkin);
        if (_skinNumber == SkinManager.skinManager.GetTotalSkinCount() - 1)
        {
            _skinNumber = 0;
        }
        else
        {
            _skinNumber++;
        }
        SkinManager.skinManager.SetActiveSkin(_skinNumber);
        UpdateSkin();
        UpdateText();
    }

    public void PrevSkin()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ScrollSkin);
        if (_skinNumber == 0)
        {
            _skinNumber = SkinManager.skinManager.GetTotalSkinCount() - 1;
        }
        else
        {
            _skinNumber--;
        }
        SkinManager.skinManager.SetActiveSkin(_skinNumber);
        UpdateSkin();
        UpdateText();
    }

    public void SetSkin()
    {
        if (CheckIfAvailable(_skinNumber))
        {
            SoundManager.soundManager.SingleSound(SoundSample.SetSkin);
            SkinManager.skinManager.SetActiveSkin(_skinNumber);
            SkinManager.skinManager.SaveActiveSkin();
            //buy_set_button.interactable = false;
            colorBuy.Animate();
        }
        else
        {
            if (GlobalScore.globalScore.Score >= SkinManager.skinManager.GetSkinByNumber(_skinNumber).price)
            {
                SoundManager.soundManager.SingleSound(SoundSample.Buy);
                GlobalScore.globalScore.Score -= SkinManager.skinManager.GetSkinByNumber(_skinNumber).price;
                PlayerPrefs.SetInt(SkinManager.skinManager.GetSkinByNumber(_skinNumber).name, 1);

                //сразу активируем
                SkinManager.skinManager.SetActiveSkin(_skinNumber);
                SkinManager.skinManager.SaveActiveSkin();

                colorBuy.Animate();
            }
            else
            {
                SoundManager.soundManager.SingleSound(SoundSample.Error);
                moneyAnim.SetTrigger("animate");
            }
        }
        SetEquipButtonStatus();
        UpdateText();
    }

    void OnEnable()
    {
        //print("fg");
        InitMenu();
    }

    void InitMenu()
    {
        _skinNumber = SkinManager.skinManager.GetSkinNumber();
        SkinManager.skinManager.SetActiveSkin(_skinNumber);
        for (int i = 0; i < SkinManager.skinManager.GetTotalSkinCount(); i++)
        {
            if ((!CheckIfAvailable(i)) && (SkinManager.skinManager.GetSkinByNumber(i).price == 0))
            {
                PlayerPrefs.SetInt(SkinManager.skinManager.GetSkinByNumber(i).name, 1);
                PlayerPrefs.Save();
            }
            
        }
        UpdateSkin();
        UpdateText();
    }

    void UpdateSkin()
    {
        SetEquipButtonStatus();
        for (int i = 0; i < sectors.Length;i++)
        {
            sectors[i].color = SkinManager.skinManager.GetSkinByNumber(_skinNumber).colors[i];
        }
        //BG.color = SkinManager.skin_manager.GetSkinByNumber(skin_number).bg_color;
    }

    void UpdateText()
    {
        skinCountText.text = (_skinNumber+1)+"/"+SkinManager.skinManager.GetTotalSkinCount();
        priceText.text = SkinManager.skinManager.GetSkinByNumber(_skinNumber).price.ToString();
        scoreText.text = GlobalScore.globalScore.Score.ToString();
        if (CheckIfAvailable(_skinNumber))
        {
            setButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            setButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }

    bool CheckIfAvailable(int number)
    {
        if (PlayerPrefs.HasKey(SkinManager.skinManager.GetSkinByNumber(number).name))
        {
            return true;
        }
        
        return false;
    }

    void SetEquipButtonStatus()
    {
        if (_skinNumber == PlayerPrefs.GetInt("SavedSkin"))
        {
            buySetButton.interactable = false;
        }
        else
        {
            buySetButton.interactable = true;
        }
    }
}
