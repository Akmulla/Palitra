using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController ui;
    public Text current_lvl;
    //static bool is_paused;
    GraphicRaycaster raycaster;

    public GameObject pause_menu;
    public GameObject skin_menu;
    public GameObject start_menu;
    public GameObject Game_UI;
    public GameObject round;
    public GameObject triangle_fon;
    public GameObject triangle;
    //public GameObject gameover_text;
    //public GameObject pause_text;
    //public GameObject gameover_pic;
   // public GameObject pause_pic;
    //public GameObject gameui_bg;
    public GameObject continue_replay;
    public GameObject continue_video;
    public GameObject life_timer;
    public GameObject life_menu;
    public GameObject rubin_menu;

    void Awake ()
    {
        //is_paused = false;
        ui = this;
        raycaster = GetComponent<GraphicRaycaster>();
    }

    public void OpenLifeMenu()
    {
        life_menu.SetActive(true);
    }

    public void CloseLifeMenu()
    {
        life_menu.SetActive(false);
    }

    public void OpenRubinMenu()
    {
        rubin_menu.SetActive(true);
    }

    public void CloseRubinMenu()
    {
        rubin_menu.SetActive(false);
    }


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
                life_timer.SetActive(true);
                life_menu.SetActive(false);
                break;

            case GameState.SkinMenu:
                Game_UI.SetActive(false);
                skin_menu.SetActive(true);
                start_menu.SetActive(false);
                pause_menu.SetActive(false);
                round.SetActive(false);
                triangle.SetActive(false);
                triangle_fon.SetActive(false);
                life_timer.SetActive(false);
                life_menu.SetActive(false);
                break;

            case GameState.Game:
               // gameui_bg.SetActive(true);
                raycaster.enabled = true;
                Game_UI.SetActive(true);
                skin_menu.SetActive(false);
                start_menu.SetActive(false);
                pause_menu.SetActive(false);
                round.SetActive(true);
                triangle.SetActive(true);
                triangle_fon.SetActive(false);
                Ball.ball.EnableImage();
                life_timer.SetActive(false);
                life_menu.SetActive(false);
                break;

            case GameState.Prepare:
               // gameui_bg.SetActive(false);
                raycaster.enabled = false;
                break;

            case GameState.Pause:
                raycaster.enabled = false;
                break;

            case GameState.GameOver:
               // gameui_bg.SetActive(false);
                raycaster.enabled = true;
                Game_UI.SetActive(true);
                skin_menu.SetActive(false);
                start_menu.SetActive(false);
                pause_menu.SetActive(true);
                round.SetActive(false);
                triangle.SetActive(false);
                triangle_fon.SetActive(false);

                if (GameController.game_controller.continued)
                {
                    continue_replay.SetActive((true));
                    continue_video.SetActive((false));
                }
                else
                {
                    continue_replay.SetActive((false));
                    continue_video.SetActive((true));
                }
                    //pause_text.SetActive(false);
                    //gameover_text.SetActive(true);
                    // pause_pic.SetActive(false);
                    //gameover_pic.SetActive(true);
                life_timer.SetActive(true);
                life_menu.SetActive(false);
                break;

          
        }
    }
}
