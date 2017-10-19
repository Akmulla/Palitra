using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType { Normal, Switch, Blocks,Multiple1Part, Multiple2Parts, Multiple3Parts,
    Combo3Parts,Combo4Parts,Combo5Parts, Count }

public class SpawnWaves : MonoBehaviour
{
    public static SpawnWaves spawn;
    public float prevEdge;
    //float start_delay = 2.0f;

    LineHandler[] _lineHandler;

    [Space(20)]
    public float startWait;

    List<PoolType> _lines = new List<PoolType>();
    int _normalLinesCount;

    [HideInInspector]
    public float dist;
    public float edge;
    public float offset = 0.5f;
    public int linesPassed;
    public int linesSpawned;

    bool _isSpawning;
    LvlData _lvlData;

    float Dist
    {
        get
        {
            return dist;
        }

        set
        {
            dist = value;
        }
    }

    void Awake()
    {
        spawn = this;

    }

    void Start()
    {
        _lvlData = GameController.gameController.GetLvlData();
        Pool[] pool;
        pool = GetComponentsInChildren<Pool>();

        _lineHandler = new LineHandler[(int)PoolType.Count];
        for (int i = 0; i < _lineHandler.Length; i++)
        {
            _lineHandler[i] = ScriptableObject.CreateInstance<LineHandler>();
        }

        for (int i = 0; i < (int)PoolType.Count; i++)
        {
            _lineHandler[i].poolType = (PoolType)i;
            for (int k = 0; k < (int)PoolType.Count; k++)
            {
                if (pool[k].poolType == (PoolType)i)
                {
                    _lineHandler[i].pool = pool[k];
                    break;
                }
            }
        }  
    }

    void EndGame()
    {
        //is_spawning = false;
       // lines_spawned = lines_passed;
    }

    void OnEnable()
    {
        EventManager.StartListening("LinePassed", LinePassed);
        EventManager.StartListening("ChangeLvl", ChangeLvl);
        EventManager.StartListening("EndGame", EndGame);
    }

    void OnDisable()
    { 
        EventManager.StopListening("LinePassed", LinePassed);
        EventManager.StopListening("ChangeLvl", ChangeLvl);
        EventManager.StopListening("EndGame", EndGame);
    }

    void GetLineCountData()
    {
        _lineHandler[(int)PoolType.Normal].count = GameController.gameController.GetLvlData().lineProp.count;
        _lineHandler[(int)PoolType.Switch].count = GameController.gameController.GetLvlData().switchProp.count;
        _lineHandler[(int)PoolType.Blocks].count = GameController.gameController.GetLvlData().blockProp.count;
        _lineHandler[(int)PoolType.Multiple1Part].count = GameController.gameController.GetLvlData().multipleProp1Part.count;
        _lineHandler[(int)PoolType.Multiple2Parts].count = GameController.gameController.GetLvlData().multipleProp2Parts.count;
        _lineHandler[(int)PoolType.Multiple3Parts].count = GameController.gameController.GetLvlData().multipleProp3Parts.count;
        _lineHandler[(int)PoolType.Combo3Parts].count = GameController.gameController.GetLvlData().comboProp3Parts.count;
        _lineHandler[(int)PoolType.Combo4Parts].count = GameController.gameController.GetLvlData().comboProp4Parts.count;
        _lineHandler[(int)PoolType.Combo5Parts].count = GameController.gameController.GetLvlData().comboProp5Parts.count;
    }

    void ReserveLines()
    {
        _lines.Clear();
        GetLineCountData();

        //заполняем массив линий
        for (int i=0;i<(int)PoolType.Count;i++)
        {
            for (int j=0;j<_lineHandler[i].count;j++)
            {
                _lines.Add(_lineHandler[i].poolType);
            }
        }
    }

    void LinePassed()
    {
        linesPassed++;
        //if (lines_passed >= GameController.game_controller.GetLvlData().lines_to_chng_dist)
        //{
        //    lines_passed = 0;
        //    Dist -= GameController.game_controller.GetLvlData().chng_dist_val;
        //}
        LvlType lvlType = GameController.gameController.GetLvlData().lvlType;
        bool chngAftrHalf = ((lvlType == LvlType.SpeedDecrDistDecrHalf) ||
            (lvlType == LvlType.SpeedIncrDistIncrHalf)) ? true : false;
        bool passedHalf = (spawn.GetLineSpawnedNumber() >=
            GameController.gameController.GetLvlData().totalLineCount / 2);

        float k = (chngAftrHalf && passedHalf) ? -1.0f : 1.0f;

        Dist += _lvlData.stepDist*k;
    }

    void ChangeLvl()
    {
        _lvlData = GameController.gameController.GetLvlData();
        _isSpawning = false;
        linesPassed = 0;
        linesSpawned = 0;
        prevEdge = Ball.ball.GetPosition().y;

        ReserveLines();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        //print("sdg");
        _isSpawning = false;
        //if (GameController.game_controller.GetCurrentLvl()!=0)
        //    yield return new WaitForSeconds(start_delay);
        while (GameController.gameController.GetState() != GameState.Game)
            yield return null;
        yield return new WaitForSeconds(2.0f);

        // Dist = GameController.game_controller.GetLvlData().max_dist;
        Dist = _lvlData.dist;
        edge = Edges.topEdge + offset;
        _isSpawning = true;
    }

    void Update()
    {
        if (linesSpawned >= _lvlData.totalLineCount)
        {
            _isSpawning = false;
        }

        if ((_isSpawning)&&(Edges.topEdge >= edge-offset))
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        PoolType currentLine = _lines[Random.Range(0,_lines.Count)];
        _lines.Remove(currentLine);
        
        linesSpawned++;
        
        GameObject line=_lineHandler[(int)currentLine].pool.Activate(new Vector2(0.0f, edge), Quaternion.identity);

        //line.GetComponent<Line>().InitLine();
        line.GetComponent<Line>().Enable();
        prevEdge = edge;
        edge += Dist;

    }

    public int GetLinePassedNumber()
    {
        return linesPassed;
    }

    public int GetLineSpawnedNumber()
    {
        return linesSpawned;
    }
}
