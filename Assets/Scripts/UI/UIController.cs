using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController ui;
    public Text current_lvl;
    static bool is_paused;
    GraphicRaycaster raycaster;
    //static float saved_time_scale=1.0f;

    public GameObject pause_menu;
    public GameObject skin_menu;
    public GameObject start_menu;
    public GameObject Game_UI;
    public GameObject round;
    public GameObject triangle_fon;
    public GameObject triangle;



    public GameObject gameover_text;
    public GameObject pause_text;

    public GameObject gameover_pic;
    public GameObject pause_pic;

    public GameObject gameui_bg;

    void Awake ()
    {
        is_paused = false;
        ui = this;
        raycaster = GetComponent<GraphicRaycaster>();
    }
	
    //public IEnumerator UpdateUICor()
    //{
    //    UpdateUI();
    //    yield return new WaitForEndOfFrame();
    //}
    public void UpdateUI()
    {
        switch(GameController.game_controller.GetState())
        {
            case GameState.MainMenu:
                raycaster.enabled = true;
                SkinManager.skin_manager.LoadSavedSkin();
                Game_UI.SetActive(false);
                skin_menu.SetActive(false);
                start_menu.SetActive(true);
                pause_menu.SetActive(false);
                round.SetActive(true);
                triangle.SetActive(true);
                triangle_fon.SetActive(true);
                break;

            case GameState.SkinMenu:
                Game_UI.SetActive(false);
                skin_menu.SetActive(true);
                start_menu.SetActive(false);
                pause_menu.SetActive(false);
                round.SetActive(false);
                triangle.SetActive(false);
                triangle_fon.SetActive(false);
                break;

            case GameState.Game:
                gameui_bg.SetActive(true);
                raycaster.enabled = true;
                Game_UI.SetActive(true);
                skin_menu.SetActive(false);
                start_menu.SetActive(false);
                pause_menu.SetActive(false);
                round.SetActive(true);
                triangle.SetActive(true);
                triangle_fon.SetActive(false);
                Ball.ball.EnableImage();
                break;

            case GameState.Prepare:
                gameui_bg.SetActive(false);
                raycaster.enabled = false;
                break;
            case GameState.Pause:
                gameui_bg.SetActive(false);
                raycaster.enabled = true;
                Game_UI.SetActive(true);
                skin_menu.SetActive(false);
                start_menu.SetActive(false);
                pause_menu.SetActive(true);
                round.SetActive(false);
                triangle.SetActive(false);
                triangle_fon.SetActive(false);
                pause_text.SetActive(true);
                gameover_text.SetActive(false);
                pause_pic.SetActive(true);
                gameover_pic.SetActive(false);
                current_lvl.text = GameController.game_controller.GetCurrentLvl().ToString();
                break;

            case GameState.GameOver:
                gameui_bg.SetActive(false);
                raycaster.enabled = true;
                Game_UI.SetActive(true);
                skin_menu.SetActive(false);
                start_menu.SetActive(false);
                pause_menu.SetActive(true);
                round.SetActive(false);
                triangle.SetActive(false);
                triangle_fon.SetActive(false);
                pause_text.SetActive(false);
                gameover_text.SetActive(true);
                pause_pic.SetActive(false);
                gameover_pic.SetActive(true);
                break;
        }
    }

    //public void Game()
    //{
    //    SkinMenu.SetActive(false);
    //    StartMenu.SetActive(false);
    //    pause_menu.SetActive(false);
    //    Game_UI.SetActive(true);

    //    Time.timeScale = saved_time_scale;

    //    is_paused = false;

    //    if (GameController.game_controller.GetState()==GameState.GameOver)
    //    {

    //    }
    //    //if (GameController.game_controller.gameIsOver)
    //    //{
    //    //   // SceneManager.LoadScene("New Main");
    //    //}
    //}

    //public void OpenSkinMenu()
    //{
    //    //SceneManager.LoadScene("Main");
    //    SkinMenu.SetActive(true);
    //    StartMenu.SetActive(false);
    //}
}
