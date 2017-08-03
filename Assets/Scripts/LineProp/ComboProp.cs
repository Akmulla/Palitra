using UnityEngine;
using System.Collections;

[System.Serializable]
public class ComboProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Значение, до которого замедляется шар")]
    public float slowing;
}
