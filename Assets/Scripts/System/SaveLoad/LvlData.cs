using UnityEngine;

public enum LvlType
{
    SpeedIncr, DistDecr, SpeedIncrDistIncr, SpeedDecrDistDecr,
    SpeedIncrDistIncrHalf, SpeedDecrDistDecrHalf, Count
}
[CreateAssetMenu]
public class LvlData : ScriptableObject
{
    [Header("Dist")]
    public float dist;
    public float stepDist;

    [Header("Ball")]
    public float speed;
    public float stepSpeed;

    //[Header("Coins")]
    //public int coins_reward;

    [Header("Lines")]

    [Tooltip("Стандартная линия")]
    public LineProp lineProp;

    [Tooltip("Полоса, меняющая цвет")]
    public SwitchProp switchProp;

    [Tooltip("Полосы, которые двигаются слева направо/справа налево")]
    public BlockProp blockProp;

    [Tooltip("Полоса, на которой написаны цифры из 1 части")]
    public MultipleProp multipleProp1Part;
    [Tooltip("Полоса, на которой написаны цифры из 2 частей")]
    public MultipleProp multipleProp2Parts;
    [Tooltip("Полоса, на которой написаны цифры из 3 частей")]
    public MultipleProp multipleProp3Parts;

    [Tooltip("Комбо полоса из 3 частей")]
    public ComboProp comboProp3Parts;
    [Tooltip("Комбо полоса из 4 частей")]
    public ComboProp comboProp4Parts;
    [Tooltip("Комбо полоса из 5 частей")]
    public ComboProp comboProp5Parts;

    //[HideInInspector]
    public int totalLineCount;

    public LvlType lvlType;
    //[Header("Sectors")]
    //public Color[] colors;
}
