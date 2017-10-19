using UnityEngine;
using UnityEngine.UI;

public class TrianChange : MonoBehaviour
{
    public Image trian;
    int _trianNumber;
    public Button buySetButton;
    public Animator moneyAnim;
    public GameObject buyButton;
    public GameObject setButton;
    public Text trianCountText;
    public Text priceText;
    public Text scoreText;
    public Text description;

    void UpdateTrian()
    {
        SetEquipButtonStatus();
        trian.sprite = TrianManager.trianManager.GetTrianByNumber(_trianNumber).sprite;

        //for (int i = 0; i < sectors.Length; i++)
        //{
        //    sectors[i].color = SkinManager.skin_manager.GetSkinByNumber(skin_number).colors[i];
        //}
        //BG.color = SkinManager.skin_manager.GetSkinByNumber(skin_number).bg_color;
    }

    void UpdateText()
    {
        trianCountText.text = (_trianNumber + 1) + "/" + TrianManager.trianManager.GetTotalTrianCount();
        priceText.text = TrianManager.trianManager.GetTrianByNumber(_trianNumber).price.ToString();
        scoreText.text = GlobalScore.globalScore.Score.ToString();
        description.text = TrianManager.trianManager.GetTrianByNumber(_trianNumber).description;
        if (CheckIfAvailable(_trianNumber))
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

    void OnEnable()
    {
        InitMenu();
    }

    void InitMenu()
    {
        _trianNumber = TrianManager.trianManager.GetTrianNumber();
        TrianManager.trianManager.SetActiveTrian(_trianNumber);
        for (int i = 0; i < TrianManager.trianManager.GetTotalTrianCount(); i++)
        {
            if ((!CheckIfAvailable(i)) && (TrianManager.trianManager.GetTrianByNumber(i).price == 0))
            {
                PlayerPrefs.SetInt(TrianManager.trianManager.GetTrianByNumber(i).name, 1);
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
        if (PlayerPrefs.HasKey(TrianManager.trianManager.GetTrianByNumber(number).name))
        {
            return true;
        }

        return false;
    }

    void SetEquipButtonStatus()
    {
        if (_trianNumber == PlayerPrefs.GetInt("SavedTrian"))
        {
            buySetButton.interactable = false;
        }
        else
        {
            buySetButton.interactable = true;
        }
    }

    public void NextTrian()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ScrollSkin);
        if (_trianNumber == TrianManager.trianManager.GetTotalTrianCount() - 1)
        {
            _trianNumber = 0;
        }
        else
        {
            _trianNumber++;
        }
        //SkinManager.skin_manager.SetActiveSkin(trian_number);
        UpdateTrian();
        UpdateText();
    }

    public void PrevTrian()
    {
        SoundManager.soundManager.SingleSound(SoundSample.ScrollSkin);
        if (_trianNumber == 0)
        {
            _trianNumber = TrianManager.trianManager.GetTotalTrianCount() - 1;
        }
        else
        {
            _trianNumber--;
        }
        //SkinManager.skin_manager.SetActiveSkin(trian_number);
        UpdateTrian();
        UpdateText();
    }

    public void SetTrian()
    {
        if (CheckIfAvailable(_trianNumber))
        {
            SoundManager.soundManager.SingleSound(SoundSample.SetSkin);
            TrianManager.trianManager.SetActiveTrian(_trianNumber);
            TrianManager.trianManager.SaveActiveTrian();
            //buy_set_button.interactable = false;
            //color_buy.Animate();
        }
        else
        {
            if (GlobalScore.globalScore.Score >= TrianManager.trianManager.GetTrianByNumber(_trianNumber).price)
            {
                SoundManager.soundManager.SingleSound(SoundSample.Buy);
                GlobalScore.globalScore.Score -= TrianManager.trianManager.GetTrianByNumber(_trianNumber).price;
                PlayerPrefs.SetInt(TrianManager.trianManager.GetTrianByNumber(_trianNumber).name, 1);

                //сразу активируем
                TrianManager.trianManager.SetActiveTrian(_trianNumber);
                TrianManager.trianManager.SaveActiveTrian();

                //color_buy.Animate();
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
}
