﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { MainMenu, SkinMenu, Prepare, Game,Pause, GameOver };

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
    public GameObject pools_obj;
    Pool[] pools;
    public bool prepared = false;
    [SerializeField]
    ParticleReload particle;

    [SerializeField]
    AnimationStatus anim_status;
    bool reload_part = false;
    int lines_passed;

    public GameState GetState()
    {
        return game_state;
    }

    void ChangeState(GameState game_state)
    {
        this.game_state = game_state;
    }

    public int GetCurrentLvl()
    {
        return lvl_number;
    }

    public void SetCurrentLvl(int lvl)
    {
        lvl_number=lvl;
    }

    public int GetLinesPassedNumber()
    {
        return lines_passed;
    }

    public void BeginGame()
    {
        if ((game_state==GameState.MainMenu)||(game_state == GameState.GameOver))
        {
            //lvl_number = 0;
            SaveLoadGame.save_load.LoadProgress();
            if (game_state == GameState.GameOver)
            {
                reload_part = true;
            }
            else
            {
                reload_part = false;
            }
            StartCoroutine(BeginGameCoroutine());
        }
    }

    IEnumerator BeginGameCoroutine()
    {
        Line_Switch.InitText();
        bool animate=false;
        if (game_state == GameState.MainMenu)
            animate = true;

        SoundManager.sound_manager.GameTheme();
        if (reload_part)
            particle.TurnOff();
        ChangeState(GameState.Prepare);
        UIController.ui.UpdateUI();
        if (animate)
        {
            EventManager.TriggerEvent("BeginGameAnimation");
        }
        yield return StartCoroutine(InitLvlCor());
        
        while (!anim_status.finished)
        {
            yield return new WaitForEndOfFrame();
        }
        if (reload_part)
            particle.TurnOn();
        ChangeState(GameState.Game);
        UIController.ui.UpdateUI();
        Line_Default.same_colors = 0;
        EventManager.TriggerEvent("BeginGame");
    }

    IEnumerator InitLvlCor()
    {
        System.GC.Collect();
        lines_passed = 0;

        if (lvl_number != 0)
            yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < sectors.Length; i++)
        {
            sectors[i].InitSector(SkinManager.skin_manager.GetCurrentSkin().colors[i]);
        }
        yield return StartCoroutine(InitLines());

        EventManager.TriggerEvent("ChangeLvl");
    }

    IEnumerator InitLines()
    {
        for (int i=0;i<pools.Length;i++)
        {
            for (int k=0;k<pools[i].size;k++)
            {
                yield return StartCoroutine(pools[i].InitLineCor(k));
            }
            yield return null;
        }
        yield return null;
    }

    void Awake()
    {
        Resources.UnloadUnusedAssets();
        pools = pools_obj.GetComponents<Pool>();
        game_state = GameState.MainMenu;
        
        game_controller = this;
        saved_time_scale = Time.timeScale;
    }

    void Start()
    {
        UIController.ui.UpdateUI();
        SaveLoadGame.save_load.LoadProgress();
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
        if (lines_passed >= GameController.game_controller.GetLvlData().total_line_count)
        {
            IncreaseLvl();
        }
    }

    void IncreaseLvl()
    {
        lvl_number++;
        SaveLoadGame.save_load.SaveProgress(lvl_number);
        if (lvl_number < lvl_data.Length)
        {
            //print("текущий уровень" + lvl_number);
            StartCoroutine(InitLvlCor());
        }
        else
        {
            //print("конец игры");
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
        if (game_state != GameState.Game)
            return;

        saved_time_scale = Time.timeScale;
        Time.timeScale = 0.0f;
        ChangeState(GameState.Pause);
        UIController.ui.UpdateUI();
    }

    public void Continue()
    {
        if (game_state != GameState.Pause)
            return;

        ChangeState(GameState.Game);
        Time.timeScale = saved_time_scale;
        UIController.ui.UpdateUI();
    }

    public void ToMainMenu()
    {
        SoundManager.sound_manager.MainMenuTheme();
        particle.TurnOn();
        ChangeState(GameState.MainMenu);
        Time.timeScale = saved_time_scale;
        
        UIController.ui.UpdateUI();
        
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
            particle.TurnOff();
            EventManager.TriggerEvent("EndGame");
        }
    }
}
