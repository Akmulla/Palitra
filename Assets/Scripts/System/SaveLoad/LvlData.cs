using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class LvlData : ScriptableObject
{
    [Header("Dist")]
    public float dist;
    public float step_dist;

    [Header("Ball")]
    public float speed;
    public float step_speed;

    [Header("Coins")]
    public int lines_to_coin;
    public int coins_reward;

    [Header("Lines")]

    [Tooltip("Стандартная линия")]
    public LineProp line_prop;

    [Tooltip("Полоса, меняющая цвет")]
    public SwitchProp switch_prop;

    [Tooltip("Полосы, которые двигаются слева направо/справа налево")]
    public BlockProp block_prop;

    [Tooltip("Полоса, на которой написаны цифры из 1 части")]
    public MultipleProp multiple_prop_1_part;
    [Tooltip("Полоса, на которой написаны цифры из 2 частей")]
    public MultipleProp multiple_prop_2_parts;
    [Tooltip("Полоса, на которой написаны цифры из 3 частей")]
    public MultipleProp multiple_prop_3_parts;

    [Tooltip("Комбо полоса из 3 частей")]
    public ComboProp combo_prop_3_parts;
    [Tooltip("Комбо полоса из 4 частей")]
    public ComboProp combo_prop_4_parts;
    [Tooltip("Комбо полоса из 5 частей")]
    public ComboProp combo_prop_5_parts;

    [HideInInspector]
    public int lines_to_chng_lvl;

    //[Header("Sectors")]
    //public Color[] colors;
}
