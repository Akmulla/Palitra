using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMenuUI : MonoBehaviour
{
    [SerializeField] GameObject infoMenu;
    [SerializeField] GameObject toSkinButton;
    [SerializeField] GameObject mainSkinMenu;



    public void OpenInfoMenu()
    {
        infoMenu.SetActive(true);
        toSkinButton.SetActive(true);
        mainSkinMenu.SetActive(false);
    }

    public void CloseInfoMenu()
    {
        infoMenu.SetActive(false);
        toSkinButton.SetActive(false);
        mainSkinMenu.SetActive(true);
    }
}
