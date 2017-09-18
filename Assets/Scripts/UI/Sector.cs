using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    Color sect_color;
    //static float prev_touch = 0.0f;
   // bool clicked = false;
    
    void Start()
    {
        sect_color = GetComponent<Image>().color;
    }
    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (
        //    (GameController.game_controller.GetState() == GameState.Game)
        //    //&& (Time.time - prev_touch > 0.15f)
        //   // &&(!clicked)
        //    )
        //{
        //   // prev_touch = Time.time;
        //   // clicked = true;
        //    Ball.ball.SetColor(sect_color, true);
        //    //print("click");
        //}
           
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((GameController.game_controller.GetState() == GameState.Game)
            //&& (Time.time - prev_touch > 0.15f)
            //&& (!clicked)
            )
        {
            //  prev_touch = Time.time;
            // clicked = true;
            Ball.ball.SetColor(sect_color, false);
        }

    }

    public void InitSector(Color color)
    {
        GetComponent<Image>().color = color;
        sect_color = color;
    }
    #endregion


    //void LateUpdate()
    //{
    //    clicked = false;
    //}
}