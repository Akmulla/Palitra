using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skin_manager;
    [SerializeField]
    SkinData[] skin_data;
    int totalSkinCount=1;
    int active_skin=0;

    public int GetSkinNumber()
    {
        return active_skin;
    }

    public void SetActiveSkin(int new_skin_number)
    {
        if (new_skin_number!=active_skin)
        {
            active_skin = Mathf.Clamp(new_skin_number, 0, totalSkinCount);
            EventManager.TriggerEvent("SkinChanged");
        }
    }

    public void SaveActiveSkin()
    {
        PlayerPrefs.SetInt("SavedSkin", active_skin);
        PlayerPrefs.Save();
    }

    public int GetTotalSkinCount()
    {
        return totalSkinCount;
    }

    public void LoadSavedSkin()
    {
        if (!PlayerPrefs.HasKey("SavedSkin"))
        {
            PlayerPrefs.SetInt("SavedSkin", active_skin);
        }
        PlayerPrefs.Save();
        SetActiveSkin(PlayerPrefs.GetInt("SavedSkin"));
    }

    void Awake()
    {
        active_skin = 0;
        skin_manager = this;
        totalSkinCount = skin_data.Length;
        if (PlayerPrefs.HasKey("SavedSkin"))
        {
            active_skin=PlayerPrefs.GetInt("SavedSkin");
        }
        else
        {
            PlayerPrefs.SetInt("SavedSkin", active_skin);
        }
        PlayerPrefs.Save();
    }

    public SkinData GetCurrentSkin()
    {
        return skin_data[active_skin];
    }

    public SkinData GetSkinByNumber(int number)
    {
        return skin_data[number];
    }
}



