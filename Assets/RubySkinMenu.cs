using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubySkinMenu : MonoBehaviour
{
    [SerializeField] GameObject rubyPopUp;
	
    public void Open()
    {
        rubyPopUp.SetActive(true);
    }

    public void Close()
    {
        rubyPopUp.SetActive(false);
    }
}
