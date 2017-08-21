using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

using System.Linq;


[System.Serializable]
public struct LvlParams
{
    public float default_dist;
    public Vector2 dist;
    public Vector2 speed;
    public Vector2 chng_clr_dist;
    public Vector2 chng_clr_time;
    public Vector2 block_speed;
    public Vector2 block_count;
    public Vector2 tap_amount_1;
    public Vector2 tap_slow_1;
    public Vector2 tap_amount_2;
    public Vector2 tap_slow_2;
    public Vector2 tap_amount_3;
    public Vector2 tap_slow_3;
    public Vector2 combo_slow_3;
    public Vector2 combo_slow_4;
    public Vector2 combo_slow_5;
}

public class GenerateLvls : MonoBehaviour
{
    public int cycles=1;
    public float begin_calc_dist;
    public float avail_time = 30.0f;
    public string path = "Assets/LvlData/NewLvls";
    public LvlParams top_params;
    public LvlParams bot_params;

    void Start()
    {

        //int lvl_count = (int)((start_params.dist.x - end_params.dist.x) / 0.1f);
        //int lvl_count = (int)((start_params.default_dist - end_params.default_dist) / 0.1f);
        //int lvl_count = (int)((start_params.default_dist - end_params.default_dist) / 0.1f);

        
        int cycles_passed=0;
        bool down = true;
        float cur_step = begin_calc_dist;
        float t = Mathf.InverseLerp(top_params.default_dist, bot_params.default_dist,
            begin_calc_dist);


        int lvl_count = (int)((top_params.default_dist - bot_params.default_dist) / 0.1f)+
            (int)((begin_calc_dist - bot_params.default_dist) / 0.1f)+
            (int)((top_params.default_dist - bot_params.default_dist) / 0.1f)*2*(cycles-1);

       

        List<string> res = CalcCombinations(lvl_count);
        //print(lvl_count);
        //print(lvl_count);
        int lvl_numb = 0;
        LvlType lvl_type = LvlType.Speed_incr;

        while (cycles_passed<cycles)
        {
            LvlData lvl = ScriptableObject.CreateInstance<LvlData>();
            lvl.lvl_type = lvl_type;
            //float t = (float)lvl_count / (float)lvl_count;

            CalcLineParams(lvl, t);
            SetLineCount(lvl, res[lvl_numb]);
            CalcSteps(lvl, t, lvl_type);

            AssetDatabase.CreateAsset(lvl, path + "/Lvl_" + lvl_numb.ToString() + ".asset");

            lvl_type++;
            if (lvl_type == LvlType.Count)
                lvl_type = (LvlType)0;

            AssetDatabase.SaveAssets();


            if (down)
            {
                cur_step -= 0.1f;
            }
            else
            {
                cur_step += 0.1f;
            }

            if (cur_step <= bot_params.default_dist+0.05f)
            {
                down = false;
            }

            if (cur_step >= top_params.default_dist-0.05f)
            {
                down = true;
                cycles_passed++;
            }

            t=Mathf.InverseLerp(top_params.default_dist, bot_params.default_dist,
             cur_step);
            lvl_numb++;
        }

        
        
    }

    void SetLineCount(LvlData lvl, string line_info)
    {
        lvl.line_prop.count = 0;
        lvl.switch_prop.count = 0;
        lvl.block_prop.count = 0;
        lvl.multiple_prop_1_part.count = 0;
        lvl.combo_prop_3_parts.count = 0;
        lvl.total_line_count = 0;
        float lvl_time = 0.0f;
        //float dist = Edges.topEdge - Edges.botEdge;
        float dist = lvl.dist;

        if (dist < 0.01f)
        {
            dist = 0.01f;
            print("too small dist");
        }

        while (lvl_time<avail_time)
        {
            for (int i = 0; i < line_info.Length; i++)
            {
                switch (line_info[i])
                {
                    case '1':
                        lvl.line_prop.count +=1;
                        lvl_time += dist / lvl.speed;
                        break;

                    case '2':
                        lvl.switch_prop.count += 1;
                        lvl_time += dist / lvl.speed;
                        break;

                    case '3':
                        lvl.block_prop.count += 1;
                        lvl_time += dist / lvl.speed;
                        break;

                    case '4':
                        lvl.multiple_prop_1_part.count += 1;
                        lvl_time += dist / lvl.multiple_prop_1_part.slowing;
                        break;

                    case '5':
                        lvl.combo_prop_3_parts.count += 1;
                        lvl_time += dist / lvl.combo_prop_3_parts.slowing;
                        break;

                    default:
                        print("error");
                        break;
                }
                lvl.total_line_count++;
            }
        }
       
    }

    List<string> CalcCombinations(int lvl_count)
    {
        List<string> result = new List<string>();
        List<string> base_comb = GetCombination(new List<int> { 1, 2, 3, 4, 5 });


        //туториальные уровни
        result.Add("1");
        result.Add("12");
        result.Add("13");
        result.Add("14");
        result.Add("15");



        for (int i=3;i<=4;i++)
        {
            foreach (string comb in base_comb)
            {
                if ((comb.Length==i)&&(comb.Contains("1")))
                {
                    result.Add(comb);
                }
            }
        }
        
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        result.Add("12345");
        ////////////////////////////

        //print(result.Count);
        //List<string> comb_pool = new List<string>(base_comb);
        while (result.Count< lvl_count)
        {
            for (int i = 2; i <= 5; i++)
            {
                List<string> comb_pool = GenTempPool(base_comb, i);

                for (int j = 0; j < 5; j++)
                {
                    if (comb_pool.Count != 0)
                    {
                        int k = Random.Range(0, comb_pool.Count);
                        result.Add(comb_pool[k]);
                        comb_pool.RemoveAt(k);
                    }
                    else
                    {
                        comb_pool = GenTempPool(base_comb, i);
                    }
                }
                //if (comb_pool.Any(s => s.Length == i))
                //{

                //}
            }
        }
        
        
        
        //comb_pool[0] = "0";
        //base_comb[0] = "1";

        //print(comb_pool[0]);
        //print(base_comb[0]);
        return result;
    }

    List<string> GenTempPool(List<string> base_comb, int length)
    {
        List<string> comb_pool = new List<string>();
        for (int j = 0; j < base_comb.Count; j++)
        {
            if (base_comb[j].Length == length)
            {
                comb_pool.Add(base_comb[j]);
            }
        }

        return comb_pool;
    }

    List<string> GetCombination(List<int> list)
    {
        List<string> result = new List<string>();
        double count = Mathf.Pow(2, list.Count);
        for (int i = 1; i <= count - 1; i++)
        {
            string new_str = "";
            result.Add(new_str);
            string str = System.Convert.ToString(i, 2).PadLeft(list.Count, '0');
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '1')
                {
                   // print(list[j]);
                    result[i-1] += list[j];
                }
            }
            //print("next");
           // Console.WriteLine();
        }

        return result;
    }

    void CalcSteps(LvlData lvl,float t,LvlType lvl_type)
    {
        lvl.step_dist = 0;
        lvl.step_speed = 0;
        float min_speed;
        float max_speed;
        float min_dist;
        float max_dist;


        switch (lvl_type)
        {
            case LvlType.Speed_incr:
                min_speed = lvl.speed;
                max_speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.step_speed = Mathf.Abs(max_speed - min_speed) / lvl.total_line_count;
                break;

            case LvlType.Dist_decr:
                min_dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                max_dist = lvl.dist;
                lvl.step_dist =  - Mathf.Abs(min_dist - max_dist) / lvl.total_line_count;
                break;

            case LvlType.Speed_incr_dist_incr:
                min_speed = lvl.speed;
                max_speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.step_speed = Mathf.Abs(max_speed - min_speed) / lvl.total_line_count;

                min_dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                max_dist = lvl.dist;
                lvl.step_dist = Mathf.Abs(min_dist - max_dist) / lvl.total_line_count;
                break;

            case LvlType.Speed_decr_dist_decr:
                min_speed = lvl.speed;
                max_speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.step_speed = -Mathf.Abs(max_speed - min_speed) / lvl.total_line_count;

                min_dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                max_dist = lvl.dist;
                lvl.step_dist = -Mathf.Abs(min_dist - max_dist) / lvl.total_line_count;
                break;

            case LvlType.Speed_incr_dist_incr_half:
                min_speed = lvl.speed;
                max_speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.step_speed = Mathf.Abs(max_speed - min_speed) / (lvl.total_line_count/2);

                min_dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                max_dist = lvl.dist;
                lvl.step_dist = Mathf.Abs(min_dist - max_dist) / (lvl.total_line_count/2);
                break;

            case LvlType.Speed_decr_dist_decr_half:
                min_speed = lvl.speed;
                max_speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.step_speed = -Mathf.Abs(max_speed - min_speed) / (lvl.total_line_count / 2);

                min_dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                max_dist = lvl.dist;
                lvl.step_dist = -Mathf.Abs(min_dist - max_dist) / (lvl.total_line_count / 2);
                break;

        }
        
    }

    void CalcLineParams(LvlData lvl,float t)
    {
        lvl.line_prop = new LineProp();
        switch (lvl.lvl_type)
        {
            case LvlType.Speed_incr:
                lvl.speed = Mathf.Lerp(top_params.speed.x, bot_params.speed.x, t);
                //lvl.dist = Mathf.Lerp(start_params.dist.x, end_params.dist.x, t);
                lvl.dist = Mathf.Lerp(top_params.default_dist, 
                    bot_params.default_dist, t);
                break;

            case LvlType.Dist_decr:
                lvl.speed = Mathf.Lerp(top_params.speed.x, bot_params.speed.x, t);
                lvl.dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                break;

            case LvlType.Speed_incr_dist_incr:
                lvl.speed = Mathf.Lerp(top_params.speed.x, bot_params.speed.x, t);
                lvl.dist = Mathf.Lerp(top_params.dist.x, bot_params.dist.x, t);
                break;

            case LvlType.Speed_decr_dist_decr:
                lvl.speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                break;

            case LvlType.Speed_incr_dist_incr_half:
                lvl.speed = Mathf.Lerp(top_params.speed.x, bot_params.speed.x, t);
                lvl.dist = Mathf.Lerp(top_params.dist.x, bot_params.dist.x, t);
                break;

            case LvlType.Speed_decr_dist_decr_half:
                lvl.speed = Mathf.Lerp(top_params.speed.y, bot_params.speed.y, t);
                lvl.dist = Mathf.Lerp(top_params.dist.y, bot_params.dist.y, t);
                break;

        }
        //lvl.dist= Mathf.Lerp(start_params.dist.x, end_params.dist.x, t);

        

        //lvl.speed = Mathf.Lerp(start_params.speed.x, end_params.speed.x, t);
        

        lvl.switch_prop = new SwitchProp();
        lvl.switch_prop.dist = Mathf.Lerp(top_params.chng_clr_dist.x, bot_params.chng_clr_dist.x, t);
        lvl.switch_prop.time_to_change = Mathf.Lerp(top_params.chng_clr_time.x, bot_params.chng_clr_time.x, t);

        lvl.block_prop = new BlockProp();
        lvl.block_prop.speed = Mathf.Lerp(top_params.block_speed.x, bot_params.block_speed.x, t);
        lvl.block_prop.block_count = (int)Mathf.Lerp(top_params.block_count.x, bot_params.block_count.x, t);

        lvl.multiple_prop_1_part = new MultipleProp();
        lvl.multiple_prop_1_part.min_taps = (int)Mathf.Lerp(top_params.tap_amount_1.x, bot_params.tap_amount_1.x, t);
        lvl.multiple_prop_1_part.slowing = Mathf.Lerp(top_params.tap_slow_1.x, bot_params.tap_slow_1.x, t);

        lvl.multiple_prop_2_parts = new MultipleProp();
        lvl.multiple_prop_2_parts.min_taps = (int)Mathf.Lerp(top_params.tap_amount_2.x, bot_params.tap_amount_2.x, t);
        lvl.multiple_prop_2_parts.slowing = Mathf.Lerp(top_params.tap_slow_2.x, bot_params.tap_slow_2.x, t);

        lvl.multiple_prop_3_parts = new MultipleProp();
        lvl.multiple_prop_3_parts.min_taps = (int)Mathf.Lerp(top_params.tap_amount_3.x, bot_params.tap_amount_3.x, t);
        lvl.multiple_prop_3_parts.slowing = Mathf.Lerp(top_params.tap_slow_3.x, bot_params.tap_slow_3.x, t);

        lvl.combo_prop_3_parts = new ComboProp();
        lvl.combo_prop_3_parts.slowing= Mathf.Lerp(top_params.combo_slow_3.x, bot_params.combo_slow_3.x, t);

        lvl.combo_prop_4_parts = new ComboProp();
        lvl.combo_prop_4_parts.slowing = Mathf.Lerp(top_params.combo_slow_4.x, bot_params.combo_slow_4.x, t);

        lvl.combo_prop_5_parts = new ComboProp();
        lvl.combo_prop_5_parts.slowing = Mathf.Lerp(top_params.combo_slow_5.x, bot_params.combo_slow_5.x, t);
    }
}

#endif
