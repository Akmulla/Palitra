using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public static UI_MainMenu ui;
    public GameObject SkinMenu;
    public GameObject StartMenu;

    void Awake()
    {
        ui = this;
    }

    public void BeginGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    IEnumerator BeginGameCoroutine()
    {
        EventManager.TriggerEvent("BeginGameAnimation");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Main");
    }

    public void OpenSkinMenu()
    {
        SkinMenu.SetActive(true);
        StartMenu.SetActive(false);
        
    }

    public void OpenStartMenu()
    {
        SkinManager.skin_manager.LoadSavedSkin();
        SkinMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

    void OnAwake()
    {
        OpenStartMenu();
    }
}
