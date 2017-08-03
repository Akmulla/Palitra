using UnityEngine;
using System.Collections;

[System.Serializable]
public class MultipleProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Минимально возможное кол-во нажатий, которое необходимо для прохождения")]
    public int min_taps;
    [Tooltip("Максимально возможное кол-во нажатий, которое необходимо для прохождения")]
    public int max_taps;
    [Tooltip("Значение, до которого замедляется шар")]
    public float slowing;
}
