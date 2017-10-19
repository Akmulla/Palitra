using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinManager;
    [SerializeField]
    SkinData[] _skinData;
    int _totalSkinCount = 1;
    int _activeSkin;

    public int GetSkinNumber()
    {
        return _activeSkin;
    }

    public void SetActiveSkin(int newSkinNumber)
    {
        if (newSkinNumber!=_activeSkin)
        {
            _activeSkin = Mathf.Clamp(newSkinNumber, 0, _totalSkinCount);
            EventManager.TriggerEvent("SkinChanged");
        }
    }

    public void SaveActiveSkin()
    {
        PlayerPrefs.SetInt("SavedSkin", _activeSkin);
        PlayerPrefs.Save();
    }

    public int GetTotalSkinCount()
    {
        return _totalSkinCount;
    }

    public void LoadSavedSkin()
    {
        if (!PlayerPrefs.HasKey("SavedSkin"))
        {
            PlayerPrefs.SetInt("SavedSkin", _activeSkin);
        }
        PlayerPrefs.Save();
        SetActiveSkin(PlayerPrefs.GetInt("SavedSkin"));
    }

    void Awake()
    {
        _activeSkin = 0;
        skinManager = this;
        _totalSkinCount = _skinData.Length;
        if (PlayerPrefs.HasKey("SavedSkin"))
        {
            _activeSkin = PlayerPrefs.GetInt("SavedSkin");
        }
        else
        {
            PlayerPrefs.SetInt("SavedSkin", _activeSkin);
        }
        PlayerPrefs.Save();
    }

    public SkinData GetCurrentSkin()
    {
        return _skinData[_activeSkin];
    }

    public SkinData GetSkinByNumber(int number)
    {
        return _skinData[number];
    }
}



