using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Скорость горизонтального движения блоков")]
    public float speed;
    [Tooltip("Число блоков")]
    public int block_count;
}
