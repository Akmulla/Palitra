﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PoolType { Normal, Switch, Blocks,Multiple_1_part, Multiple_2_parts, Multiple_3_parts,
    Combo_3_parts,Combo_4_parts,Combo_5_parts, Count };

public class SpawnWaves : MonoBehaviour
{
    public static SpawnWaves spawn;
    public float prev_edge = 0.0f;
    //float start_delay = 2.0f;

    LineHandler[] line_handler;

    [Space(20)]
    public float startWait;

    List<PoolType> lines = new List<PoolType>();
    int normal_lines_count;

    [HideInInspector]
    public float dist;
    public float edge;
    public float offset = 0.5f;
    public int lines_passed;
    public int lines_spawned;

    bool is_spawning=false;
    LvlData lvl_data;

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
        lvl_data = GameController.game_controller.GetLvlData();
        Pool[] pool;
        pool = GetComponentsInChildren<Pool>();

        line_handler = new LineHandler[(int)PoolType.Count];
        for (int i = 0; i < line_handler.Length; i++)
        {
            line_handler[i] = ScriptableObject.CreateInstance<LineHandler>();
        }

        for (int i = 0; i < (int)PoolType.Count; i++)
        {
            line_handler[i].pool_type = (PoolType)i;
            for (int k = 0; k < (int)PoolType.Count; k++)
            {
                if (pool[k].pool_type == (PoolType)i)
                {
                    line_handler[i].pool = pool[k];
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
        line_handler[(int)PoolType.Normal].count = GameController.game_controller.GetLvlData().line_prop.count;
        line_handler[(int)PoolType.Switch].count = GameController.game_controller.GetLvlData().switch_prop.count;
        line_handler[(int)PoolType.Blocks].count = GameController.game_controller.GetLvlData().block_prop.count;
        line_handler[(int)PoolType.Multiple_1_part].count = GameController.game_controller.GetLvlData().multiple_prop_1_part.count;
        line_handler[(int)PoolType.Multiple_2_parts].count = GameController.game_controller.GetLvlData().multiple_prop_2_parts.count;
        line_handler[(int)PoolType.Multiple_3_parts].count = GameController.game_controller.GetLvlData().multiple_prop_3_parts.count;
        line_handler[(int)PoolType.Combo_3_parts].count = GameController.game_controller.GetLvlData().combo_prop_3_parts.count;
        line_handler[(int)PoolType.Combo_4_parts].count = GameController.game_controller.GetLvlData().combo_prop_4_parts.count;
        line_handler[(int)PoolType.Combo_5_parts].count = GameController.game_controller.GetLvlData().combo_prop_5_parts.count;
    }

    void ReserveLines()
    {
        lines.Clear();
        GetLineCountData();

        //заполняем массив линий
        for (int i=0;i<(int)PoolType.Count;i++)
        {
            for (int j=0;j<line_handler[i].count;j++)
            {
                lines.Add(line_handler[i].pool_type);
            }
        }
    }

    void LinePassed()
    {
        lines_passed++;
        //if (lines_passed >= GameController.game_controller.GetLvlData().lines_to_chng_dist)
        //{
        //    lines_passed = 0;
        //    Dist -= GameController.game_controller.GetLvlData().chng_dist_val;
        //}
        LvlType lvl_type = GameController.game_controller.GetLvlData().lvl_type;
        bool chng_aftr_half = ((lvl_type == LvlType.Speed_decr_dist_decr_half) ||
            (lvl_type == LvlType.Speed_incr_dist_incr_half)) ? true : false;
        bool passed_half = (SpawnWaves.spawn.GetLineSpawnedNumber() >=
            GameController.game_controller.GetLvlData().total_line_count / 2);

        float k = (chng_aftr_half && passed_half) ? -1.0f : 1.0f;

        Dist += lvl_data.step_dist*k;
    }

    void ChangeLvl()
    {
        lvl_data = GameController.game_controller.GetLvlData();
        is_spawning = false;
        lines_passed = 0;
        lines_spawned = 0;
        prev_edge = Ball.ball.GetPosition().y;

        ReserveLines();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        //print("sdg");
        is_spawning = false;
        //if (GameController.game_controller.GetCurrentLvl()!=0)
        //    yield return new WaitForSeconds(start_delay);
        while (GameController.game_controller.GetState() != GameState.Game)
            yield return null;
        yield return new WaitForSeconds(2.0f);

        // Dist = GameController.game_controller.GetLvlData().max_dist;
        Dist = lvl_data.dist;
        edge = Edges.topEdge + offset;
        is_spawning = true;
    }

    void Update()
    {
        if (lvl_data == null)
            return;
        if (lines_spawned >= lvl_data.total_line_count)
        {
            is_spawning = false;
        }

        if ((is_spawning)&&(Edges.topEdge >= edge-offset))
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        PoolType current_line = lines[Random.Range(0,lines.Count)];
        lines.Remove(current_line);
        
        lines_spawned++;
        
        GameObject line=line_handler[(int)current_line].pool.Activate(new Vector2(0.0f, edge), Quaternion.identity);

        //line.GetComponent<Line>().InitLine();
        line.GetComponent<Line>().Enable();
        prev_edge = edge;
        edge += Dist;

    }

    public int GetLinePassedNumber()
    {
        return lines_passed;
    }

    public int GetLineSpawnedNumber()
    {
        return lines_spawned;
    }
}
