using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseSkinRuby : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RubySkinMenu ruby;

    public void OnPointerClick(PointerEventData eventData)
    {
        ruby.Close();
    }
}


