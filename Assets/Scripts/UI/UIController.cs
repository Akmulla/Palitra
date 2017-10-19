using System;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController ui;
    public Text currentLvl;
    //static bool is_paused;
    GraphicRaycaster _raycaster;

    public GameObject pauseMenu;
    public GameObject skinMenu;
    public GameObject startMenu;
    public GameObject gameUi;
    public GameObject round;
    public GameObject triangleFon;
    public GameObject triangle;
    public GameObject gameoverPic;
    public GameObject pausePic;
    public GameObject gameuiBg;
    public GameObject lifeTimer;
    public GameObject lifeMenu;
    public GameObject rubinMenu;

    void Awake ()
    {
        //is_paused = false;
        ui = this;
        _raycaster = GetComponent<GraphicRaycaster>();
    }

    public void OpenLifeMenu()
    {
        
    }

    public void CloseLifeMenu()
    {
        
    }

    public void OpenRubinMenu()
    {

    }

    public void CloseRubinMenu()
    {

    }

    public void UpdateUi()
    {
        switch(GameController.gameController.GetState())
        {
            case GameState.MainMenu:
                _raycaster.enabled = true;
                SkinManager.skinManager.LoadSavedSkin();
                gameUi.SetActive(false);
                skinMenu.SetActive(false);
                startMenu.SetActive(true);
                pauseMenu.SetActive(false);
                round.SetActive(true);
                triangle.SetActive(true);
                triangleFon.SetActive(true);
                lifeTimer.SetActive(true);
                lifeMenu.SetActive(false);
                break;

            case GameState.SkinMenu:
                gameUi.SetActive(false);
                skinMenu.SetActive(true);
                startMenu.SetActive(false);
                pauseMenu.SetActive(false);
                round.SetActive(false);
                triangle.SetActive(false);
                triangleFon.SetActive(false);
                lifeTimer.SetActive(false);
                break;

            case GameState.Game:
                gameuiBg.SetActive(true);
                _raycaster.enabled = true;
                gameUi.SetActive(true);
                skinMenu.SetActive(false);
                startMenu.SetActive(false);
                pauseMenu.SetActive(false);
                round.SetActive(true);
                triangle.SetActive(true);
                triangleFon.SetActive(false);
                Ball.ball.EnableImage();
                lifeTimer.SetActive(false);
                break;

            case GameState.Prepare:
                gameuiBg.SetActive(false);
                _raycaster.enabled = false;
                break;

            case GameState.Pause:
                _raycaster.enabled = false;
                break;

            case GameState.GameOver:
                gameuiBg.SetActive(false);
                _raycaster.enabled = true;
                gameUi.SetActive(true);
                skinMenu.SetActive(false);
                startMenu.SetActive(false);
                pauseMenu.SetActive(true);
                round.SetActive(false);
                triangle.SetActive(false);
                triangleFon.SetActive(false);
                pausePic.SetActive(false);
                gameoverPic.SetActive(true);
                lifeTimer.SetActive(true);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
