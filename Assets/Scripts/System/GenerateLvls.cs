using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public struct LvlParams
{
    public float dist_min;
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

    void Start()
    {

        int lvl_count = (int)((start_params.dist_min - end_params.dist_min) / 0.1f);

        for (int lvl_number = 0; lvl_number <= lvl_count; lvl_number++)
        {
            // LvlData lvl = new LvlData();
            LvlData lvl = ScriptableObject.CreateInstance<LvlData>();
            float t = (float)lvl_number / (float)lvl_count;
            CalcLineParams(lvl, t);
            

            AssetDatabase.CreateAsset(lvl, path + "/Lvl_" + lvl_number.ToString() + ".asset");
        }

        AssetDatabase.SaveAssets();
    }

    void CalcLineParams(LvlData lvl,float t)
    {
        lvl.min_speed = Mathf.Lerp(start_params.speed.x, end_params.speed.x, t);
        lvl.max_speed = Mathf.Lerp(start_params.speed.y, end_params.speed.y, t);

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
