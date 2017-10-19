using System;
using System.Collections;
using UnityEngine;

public enum GameState { MainMenu, SkinMenu, Prepare, Game,Pause, GameOver }

public class GameController : MonoBehaviour
{
    //Hearts hearts;
    public static GameController gameController;
    float _savedTimeScale = 1.0f;
    GameState _gameState;
    //[SerializeField]
   // LvlData[] lvl_data;
    [SerializeField]
    Sector[] _sectors;
    [SerializeField]
    int _lvlNumber;
    public GameObject poolsObj;
    Pool[] _pools;
    public bool prepared;
    [SerializeField]
    ParticleReload _particle;

    [SerializeField]
    AnimationStatus _animStatus;
    bool _reloadPart;
    int _linesPassed;
    GameState _savedState;
    
    //int loaded_lvl;
    [SerializeField]
    LvlData _loadedLvlData;

    int _totalLineCount;


    public GameState GetState()
    {
        return _gameState;
    }

    void ChangeState(GameState gameState)
    {
        _gameState = gameState;
    }

 

    public int CurrentLvl
    {
        get
        {
            return _lvlNumber;
        }
        set
        {
            _lvlNumber = value;
            _loadedLvlData = Resources.Load<LvlData>("Lvl_" + _lvlNumber);
        }
    }
    //public int GetCurrentLvl()
    //{
    //    return lvl_number;
    //}

    //public void SetCurrentLvl(int lvl)
    //{
    //    lvl_number=lvl;
    //}

    public int GetLinesPassedNumber()
    {
        return _linesPassed;
    }

    public void BeginGame()
    {
        if (!Hearts.h.CheckHearts())
            return;

        if ((_gameState != GameState.MainMenu) && (_gameState != GameState.GameOver))
            return;

        //lvl_number = 0;
        SaveLoadGame.saveLoad.LoadProgress();

        _reloadPart = _gameState == GameState.GameOver;
        StartCoroutine(BeginGameCoroutine());
    }

    IEnumerator BeginGameCoroutine()
    {
        _linesPassed = 0;
        _totalLineCount = gameController.GetLvlData().totalLineCount;
        LineSwitch.InitText();
        bool animate = _gameState == GameState.MainMenu;

        SoundManager.soundManager.GameTheme();
        if (_reloadPart)
            _particle.TurnOff();
        ChangeState(GameState.Prepare);
        UiController.ui.UpdateUi();
        if (animate)
        {
            EventManager.TriggerEvent("BeginGameAnimation");
        }
        yield return StartCoroutine(InitLvlCor());
        
        while (!_animStatus.Finished)
        {
            yield return new WaitForEndOfFrame();
        }
        if (_reloadPart)
            _particle.TurnOn();
        ChangeState(GameState.Game);
        UiController.ui.UpdateUi();
        LineDefault.sameColors = 0;
        EventManager.TriggerEvent("BeginGame");
    }

    IEnumerator InitLvlCor()
    {
        GC.Collect();
        //lines_passed = 0;
        //total_line_count = GameController.game_controller.GetLvlData().total_line_count;
        if (CurrentLvl != 0)
            yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < _sectors.Length; i++)
        {
            _sectors[i].InitSector(SkinManager.skinManager.GetCurrentSkin().colors[i]);
        }
        yield return StartCoroutine(InitLines());

        EventManager.TriggerEvent("ChangeLvl");
    }

    IEnumerator InitLines()
    {
        for (int i=0;i<_pools.Length;i++)
        {
            for (int k=0;k<_pools[i].size;k++)
            {
                yield return StartCoroutine(_pools[i].InitLineCor(k));
            }
            yield return null;
        }
        yield return null;
    }

    void Awake()
    {
        Resources.UnloadUnusedAssets();
        _pools = poolsObj.GetComponents<Pool>();
        _gameState = GameState.MainMenu;
        
        gameController = this;
        _savedTimeScale = Time.timeScale;
    }

    void Start()
    {
        UiController.ui.UpdateUi();
        SaveLoadGame.saveLoad.LoadProgress();
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
        _linesPassed++;
        if ((_linesPassed >= _totalLineCount)&&(_gameState==GameState.Game))
        {
            EventManager.TriggerEvent("LvlFinished");
            //Debug.Break();
            IncreaseLvl();
        }
    }

    void IncreaseLvl()
    {
        //lvl_number++;
        CurrentLvl++;
        _linesPassed = 0;
        _totalLineCount = gameController.GetLvlData().totalLineCount;
        SaveLoadGame.saveLoad.SaveProgress(CurrentLvl);
        //if (lvl_number < lvl_data.Length)
        {
            //print("текущий уровень" + lvl_number);
            StartCoroutine(InitLvlCor());
        }
        //else
        //{
        //    //print("конец игры");
        //}
    }

    public LvlData GetLvlData()
    {
        //if (loaded_lvl!= lvl_number)
        //{
        //    loaded_lvl_data = Resources.Load<LvlData>("Lvl_" + lvl_number.ToString());

        //}
        return _loadedLvlData;
        //return lvl_data[lvl_number];
    }

    public void Pause()
    {
        _savedState = _gameState;
        _savedTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
        ChangeState(GameState.Pause);
        UiController.ui.UpdateUi();
    }

    public void Continue()
    {
        if (_gameState != GameState.Pause)
            return;

        ChangeState(_savedState);
        Time.timeScale = _savedTimeScale;
        UiController.ui.UpdateUi();
    }

    public void ResumeForBanner()
    {
        //if (game_state != GameState.GameOver)
        //    return;
        print("resume");
        ChangeState(GameState.Game);
        Time.timeScale = _savedTimeScale;
        UiController.ui.UpdateUi();
        
        EventManager.TriggerEvent("BeginGame");
        //засчитываем зафейленную линию как пройденную
        EventManager.TriggerEvent("LinePassed");
    }

    public void ToMainMenu()
    {
        SoundManager.soundManager.MainMenuTheme();
        _particle.TurnOn();
        ChangeState(GameState.MainMenu);
        Time.timeScale = _savedTimeScale;
        
        UiController.ui.UpdateUi();
        
        EventManager.TriggerEvent("EndGame");
    }

    public void ToSkinMenu()
    {
        ChangeState(GameState.SkinMenu);
        UiController.ui.UpdateUi();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        //Debug.Break();
        if (_gameState==GameState.Game)
        {
            ChangeState(GameState.GameOver);
            
            Ball.ball.Stop();
            yield return new WaitForSeconds(1.0f);

            UiController.ui.UpdateUi();
            _particle.TurnOff();
            //lines_passed = SpawnWaves.spawn.lines_passed;
            EventManager.TriggerEvent("EndGame");
            
        }
    }
}
