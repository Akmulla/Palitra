using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rate : MonoBehaviour
{

    public void RateGame()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
}
