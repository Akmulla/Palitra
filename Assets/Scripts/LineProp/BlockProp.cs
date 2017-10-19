using System;
using UnityEngine;

[Serializable]
public class BlockProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Скорость горизонтального движения блоков")]
    public float speed;
    [Tooltip("Число блоков")]
    public int blockCount;
}
