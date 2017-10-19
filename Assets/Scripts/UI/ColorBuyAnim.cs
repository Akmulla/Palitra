using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBuyAnim : MonoBehaviour
{
    public Animator anim;
	
	public void Animate()
    {
        anim.SetTrigger("ColorBuy");
    }
}
