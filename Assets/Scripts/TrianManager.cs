using UnityEngine;

public class TrianManager : MonoBehaviour
{
    public static TrianManager trianManager;
    [SerializeField]
    TrianData[] _trianData;
    int _totalTrianCount = 1;
    int _activeTrian;

    public int GetTrianNumber()
    {
        return _activeTrian;
    }

    public void SetActiveTrian(int newTrianNumber)
    {
        if (newTrianNumber != _activeTrian)
        {
            _activeTrian = Mathf.Clamp(newTrianNumber, 0, _totalTrianCount);
            //EventManager.TriggerEvent("SkinChanged");
        }
    }

    public void SaveActiveTrian()
    {
        PlayerPrefs.SetInt("SavedTrian", _activeTrian);
        PlayerPrefs.Save();
    }

    public int GetTotalTrianCount()
    {
        return _totalTrianCount;
    }

    public void LoadSavedTrian()
    {
        if (!PlayerPrefs.HasKey("SavedTrian"))
        {
            PlayerPrefs.SetInt("SavedTrian", _activeTrian);
        }
        PlayerPrefs.Save();
        SetActiveTrian(PlayerPrefs.GetInt("SavedTrian"));
    }

    void Awake()
    {
        trianManager = this;
        _activeTrian = 0;
        
        _totalTrianCount = _trianData.Length;
        if (PlayerPrefs.HasKey("SavedTrian"))
        {
            _activeTrian = PlayerPrefs.GetInt("SavedTrian");
        }
        else
        {
            PlayerPrefs.SetInt("SavedTrian", _activeTrian);
        }
        PlayerPrefs.Save();
    }

    public TrianData GetCurrentTrian()
    {
        return _trianData[_activeTrian];
    }

    public TrianData GetTrianByNumber(int number)
    {
        return _trianData[number];
    }
}
