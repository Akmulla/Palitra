using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrianType { Default,DoubleTap,Shield,DoublePoints,Screamer,HalfPoints };

[CreateAssetMenu]
public class TrianData : ScriptableObject
{
    public Sprite sprite;
    public TrianType trian_type;
    public int price;
    public string description;
}
