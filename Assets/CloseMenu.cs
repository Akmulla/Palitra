﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour, IPointerClickHandler
{
    public bool rubinMenu;
    public bool lifeMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (rubinMenu)
            UIController.ui.CloseRubinMenu();

        if (lifeMenu)
            UIController.ui.CloseLifeMenu();
    }
    
}


//PointerEventData pointerData = new PointerEventData(EventSystem.current)
//{
//    pointerId = -1,
//};

//pointerData.position = Input.GetTouch(0).position;

//List<RaycastResult> results = new List<RaycastResult>();
//EventSystem.current.RaycastAll(pointerData, results);

//if (results.Count > 0)
//{

//    print((results[0]));

//}