using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SkinData : ScriptableObject
{
    public Color[] colors=new Color[3];
    public Color bg_color;
    public int price = 0;
    
}
