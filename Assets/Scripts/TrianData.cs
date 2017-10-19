using UnityEngine;

public enum TrianType { Default,DoubleTap,Shield,DoublePoints,Screamer,HalfPoints }

[CreateAssetMenu]
public class TrianData : ScriptableObject
{
    public Sprite sprite;
    public TrianType trianType;
    public int price;
    public string description;
}
