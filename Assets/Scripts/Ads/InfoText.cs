using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    public TextMeshProUGUI Label;

    public void Start()
    {
        var info = $"Technical info:\n" +
                   $"CP: {Application.version}\n" +
                   $"AATKit version: {AATKitBinding.GetVersion()}\n" +
                   $"Appsflyer version: Unknown";
        Label.text = info;
    }
}
