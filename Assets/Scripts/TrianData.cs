using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrianType { DoubleTap,Shield,DoublePoints,Screamer,HalfPoints };

[CreateAssetMenu]
public class TrianData : ScriptableObject
{
    public Sprite sprite;
    public TrianType trian_type;
    public int price;
}
