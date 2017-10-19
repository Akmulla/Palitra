using UnityEngine;
using System.Collections;

[System.Serializable]
public class SwitchProp
{
    [Tooltip("Количество линий")]
    public int count;
    [Tooltip("Расстояние, после которого полоса перестает менять цвет")]
    public float dist;
    [Tooltip("Задержка перед сменой линии")]
    public float time_to_change;
}
