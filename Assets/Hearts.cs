using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public static Hearts h;
    [SerializeField]
    int _hearts = 10;
    public Text text;
    public Text heartMenuText;
   

    void Awake()
    {
        h = this;
        text.text = _hearts.ToString();
        heartMenuText.text = _hearts.ToString();
    }

    public int Heart
    {
        get
        {
            return _hearts;
        }
        set
        {
            _hearts = value;
            text.text = _hearts.ToString();
            heartMenuText.text= _hearts.ToString();
        }
    }

    public bool CheckHearts()
    {
        return _hearts>0;
    }

    void OnEnable()
    {
        EventManager.StartListening("BeginGame", BeginLvl);
    }

    void OnDisable()
    {
        EventManager.StopListening("BeginGame", BeginLvl);
    }

    void BeginLvl()
    {
        Heart--;
    }
}
