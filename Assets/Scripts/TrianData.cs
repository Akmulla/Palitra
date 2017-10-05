using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrianType { One,Two};

[CreateAssetMenu]
public class TrianData : ScriptableObject
{
    public Sprite sprite;
    public TrianType trian_type;
}
