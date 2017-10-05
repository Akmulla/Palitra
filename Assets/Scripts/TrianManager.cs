using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianManager : MonoBehaviour
{
    public static TrianManager trian_manager;
    [SerializeField]
    TrianData[] trian_data;
    int totalTrianCount = 1;
    int active_trian = 0;

    public int GetTrianNumber()
    {
        return active_trian;
    }

    public void SetActiveTrian(int new_trian_number)
    {
        if (new_trian_number != active_trian)
        {
            active_trian = Mathf.Clamp(new_trian_number, 0, totalTrianCount);
            //EventManager.TriggerEvent("SkinChanged");
        }
    }

    public void SaveActiveTrian()
    {
        PlayerPrefs.SetInt("SavedTrian", active_trian);
        PlayerPrefs.Save();
    }

    public int GetTotalSkinCount()
    {
        return totalTrianCount;
    }

    public void LoadSavedSkin()
    {
        if (!PlayerPrefs.HasKey("SavedTrian"))
        {
            PlayerPrefs.SetInt("SavedTrian", active_trian);
        }
        PlayerPrefs.Save();
        SetActiveTrian(PlayerPrefs.GetInt("SavedTrian"));
    }

    void Awake()
    {
        active_trian = 0;
        trian_manager = this;
        totalTrianCount = trian_data.Length;
        if (PlayerPrefs.HasKey("SavedTrian"))
        {
            active_trian = PlayerPrefs.GetInt("SavedTrian");
        }
        else
        {
            PlayerPrefs.SetInt("SavedTrian", active_trian);
        }
        PlayerPrefs.Save();
    }

    public TrianData GetCurrentTrian()
    {
        return trian_data[active_trian];
    }

    public TrianData GetTrianByNumber(int number)
    {
        return trian_data[number];
    }
}
