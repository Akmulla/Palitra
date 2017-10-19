using System;
using UnityEngine;

[Serializable]
public class ComboProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Значение, до которого замедляется шар")]
    public float slowing;
}
