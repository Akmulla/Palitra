using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { MainMenu, SkinMenu,Game, Pause, GameOver };

public class GameController : MonoBehaviour
{
    public static GameController game_controller;
    float saved_time_scale = 1.0f;
    GameState game_state;
    [SerializeField]
    LvlData[] lvl_data;
    [SerializeField]
    Sector[] sectors;
    int lvl_number;

    int lines_passed;

    public GameState GetState()
    {
        return game_state;
    }

    void ChangeState(GameState game_state)
    {
        this.game_state = game_state;
        //UIController.ui.UpdateUI();
    }


    public int GetCurrentLvl()
    {
        return lvl_number;
    }

    public int GetLinesPassedNumber()
    {
        return lines_passed;
    }

    public void BeginGame()
    {
        if ((game_state==GameState.MainMenu)||(game_state == GameState.GameOver))
        {
            lvl_number = 0;
            InitLvl();
            
            StartCoroutine(BeginGameCoroutine());
        }
    }

    IEnumerator BeginGameCoroutine()
    {
        if (game_state == GameState.MainMenu)
        {
            EventManager.TriggerEvent("BeginGameAnimation");
            yield return new WaitForSeconds(3.0f);
        }
        ChangeState(GameState.Game);
        UIController.ui.UpdateUI();
        SoundManager.sound_manager.GameTheme();
        EventManager.TriggerEvent("BeginGame");
    }

    void Awake()
    {
        Resources.UnloadUnusedAssets();
        game_state = GameState.MainMenu;
        
        game_controller = this;
        saved_time_scale = Time.timeScale;
        //EventManager.TriggerEvent("ChangeLvl");
    }

    void Start()
    {
        UIController.ui.UpdateUI();
    }

    void OnEnable()
    {
        EventManager.StartListening("LinePassed", LinePassed);
    }

    void OnDisable()
    {
        EventManager.StopListening("LinePassed", LinePassed);
    }

    void LinePassed()
    {
        lines_passed++;
        if (lines_passed >= GameController.game_controller.GetLvlData().lines_to_chng_lvl)
        {
            IncreaseLvl();
            EventManager.TriggerEvent("ChangeLvl");
        }
    }

    void IncreaseLvl()
    {
        lvl_number++;
        if (lvl_number < lvl_data.Length)
        {
            //print("текущий уровень" + lvl_number);
            InitLvl();
        }
        else
        {
           // print("конец игры");
        }
    }

    public LvlData GetLvlData()
    {
        return lvl_data[lvl_number];
    }

    void InitLvl()
    {
        System.GC.Collect();
        lines_passed = 0;

        for (int i = 0; i < sectors.Length; i++)
        {
            sectors[i].InitSector(SkinManager.skin_manager.GetCurrentSkin().colors[i]);
        }
    }

    public void Pause()
    {
        saved_time_scale = Time.timeScale;
        Time.timeScale = 0.0f;
        ChangeState(GameState.Pause);
        UIController.ui.UpdateUI();
    }

    public void Continue()
    {
        ChangeState(GameState.Game);
        Time.timeScale = saved_time_scale;
        UIController.ui.UpdateUI();
    }

    public void ToMainMenu()
    {
        ChangeState(GameState.MainMenu);
        Time.timeScale = saved_time_scale;
        
        UIController.ui.UpdateUI();
        SoundManager.sound_manager.MainMenuTheme();
        EventManager.TriggerEvent("EndGame");
    }

    public void ToSkinMenu()
    {
        ChangeState(GameState.SkinMenu);
        UIController.ui.UpdateUI();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        if (game_state==GameState.Game)
        {
            ChangeState(GameState.GameOver);
            
            Ball.ball.Stop();
            yield return new WaitForSeconds(2.0f);

            UIController.ui.UpdateUI();
            EventManager.TriggerEvent("EndGame");
        }
    }
}
