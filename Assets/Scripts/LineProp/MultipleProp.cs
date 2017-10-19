using System;
using UnityEngine;

[Serializable]
public class MultipleProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Минимально возможное кол-во нажатий, которое необходимо для прохождения")]
    public int minTaps;
    [Tooltip("Максимально возможное кол-во нажатий, которое необходимо для прохождения")]
    public int maxTaps;
    [Tooltip("Значение, до которого замедляется шар")]
    public float slowing;
}
