using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public enum LvlType { Speed_incr,Dist_decr,Count}
[System.Serializable]
public struct LvlParams
{
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
    public string path = "Assets/LvlData/NewLvls";
    public LvlParams start_params;
    public LvlParams end_params;

    float avail_time = 30.0f;

    void Start()
    {

        int lvl_count = (int)((start_params.dist.x - end_params.dist.x) / 0.1f);

        // List<string> res=GetCombination(new List<int> { 1, 2, 3,4,5 });
        List<string> res = CalcCombinations(lvl_count);
        //for (int i=0;i<res.Count;i++)
        //{
        //    print(res[i]);
        //}
        LvlType lvl_type = LvlType.Speed_incr;
        for (int lvl_number = 0; lvl_number <= lvl_count; lvl_number++)
        {
            // LvlData lvl = new LvlData();
            LvlData lvl = ScriptableObject.CreateInstance<LvlData>();
            float t = (float)lvl_number / (float)lvl_count;
            CalcLineParams(lvl, t);
            
            if (lvl_number<res.Count)
            {
                SetLineCount(lvl, res[lvl_number]);
                CalcSteps(lvl,t,lvl_type);
            }
             

            AssetDatabase.CreateAsset(lvl, path + "/Lvl_" + lvl_number.ToString() + ".asset");

            lvl_type++;
            if (lvl_type == LvlType.Count)
                lvl_type = (LvlType)0;
            
        }

        AssetDatabase.SaveAssets();
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
        switch (lvl_type)
        {
            case LvlType.Speed_incr:
                float min_speed = lvl.speed;
                float max_speed = Mathf.Lerp(start_params.speed.y, end_params.speed.y, t);
                lvl.step_speed = Mathf.Abs(max_speed - min_speed) / lvl.total_line_count;
                break;
            case LvlType.Dist_decr:
                float min_dist = Mathf.Lerp(start_params.dist.y, end_params.dist.y, t);
                float max_dist = lvl.dist;
                lvl.step_dist = Mathf.Abs(min_dist - max_dist) / lvl.total_line_count;
                break;
        }
        
    }

    void CalcLineParams(LvlData lvl,float t)
    {
        lvl.dist= Mathf.Lerp(start_params.dist.x, end_params.dist.x, t);
        lvl.line_prop = new LineProp();

        lvl.speed = Mathf.Lerp(start_params.speed.x, end_params.speed.x, t);
        

        lvl.switch_prop = new SwitchProp();
        lvl.switch_prop.dist = Mathf.Lerp(start_params.chng_clr_dist.x, end_params.chng_clr_dist.x, t);
        lvl.switch_prop.time_to_change = Mathf.Lerp(start_params.chng_clr_time.x, end_params.chng_clr_time.x, t);

        lvl.block_prop = new BlockProp();
        lvl.block_prop.speed = Mathf.Lerp(start_params.block_speed.x, end_params.block_speed.x, t);
        lvl.block_prop.block_count = (int)Mathf.Lerp(start_params.block_count.x, end_params.block_count.x, t);

        lvl.multiple_prop_1_part = new MultipleProp();
        lvl.multiple_prop_1_part.min_taps = (int)Mathf.Lerp(start_params.tap_amount_1.x, end_params.tap_amount_1.x, t);
        lvl.multiple_prop_1_part.slowing = Mathf.Lerp(start_params.tap_slow_1.x, end_params.tap_slow_1.x, t);

        lvl.multiple_prop_2_parts = new MultipleProp();
        lvl.multiple_prop_2_parts.min_taps = (int)Mathf.Lerp(start_params.tap_amount_2.x, end_params.tap_amount_2.x, t);
        lvl.multiple_prop_2_parts.slowing = Mathf.Lerp(start_params.tap_slow_2.x, end_params.tap_slow_2.x, t);

        lvl.multiple_prop_3_parts = new MultipleProp();
        lvl.multiple_prop_3_parts.min_taps = (int)Mathf.Lerp(start_params.tap_amount_3.x, end_params.tap_amount_3.x, t);
        lvl.multiple_prop_3_parts.slowing = Mathf.Lerp(start_params.tap_slow_3.x, end_params.tap_slow_3.x, t);

        lvl.combo_prop_3_parts = new ComboProp();
        lvl.combo_prop_3_parts.slowing= Mathf.Lerp(start_params.combo_slow_3.x, end_params.combo_slow_3.x, t);

        lvl.combo_prop_4_parts = new ComboProp();
        lvl.combo_prop_4_parts.slowing = Mathf.Lerp(start_params.combo_slow_4.x, end_params.combo_slow_4.x, t);

        lvl.combo_prop_5_parts = new ComboProp();
        lvl.combo_prop_5_parts.slowing = Mathf.Lerp(start_params.combo_slow_5.x, end_params.combo_slow_5.x, t);
    }
}
