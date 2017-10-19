using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMainMenu : MonoBehaviour
{
    public static UiMainMenu ui;
    public GameObject skinMenu;
    public GameObject startMenu;

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
        skinMenu.SetActive(true);
        startMenu.SetActive(false);
        
    }

    public void OpenStartMenu()
    {
        SkinManager.skinManager.LoadSavedSkin();
        skinMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    void OnAwake()
    {
        OpenStartMenu();
    }
}
