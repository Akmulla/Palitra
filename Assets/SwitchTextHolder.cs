using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTextHolder : MonoBehaviour {
    public static SwitchTextHolder holder;
    public Texture2D[] text;
    public int text_ind;

    public void InitText()
    {
        text_ind = 0;
        text = new Texture2D[3];
    }

    void Awake ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
