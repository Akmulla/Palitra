using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public class Sector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, ISelectHandler,
//IPointerDownHandler
public class Sector : Selectable
{
    Color _sectColor;
    //static float prev_touch = 0.0f;
   // bool clicked = false;
    
    void Start()
    {
        _sectColor = GetComponent<Image>().color;
    }
    //#region IPointerClickHandler implementation

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("sdg");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + " was selected");
    }
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


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if ((GameController.gameController.GetState() == GameState.Game)
            //&& (Time.time - prev_touch > 0.15f)
            //&& (!clicked)
            )
        {
            //  prev_touch = Time.time;
            // clicked = true;
            Ball.ball.SetColor(_sectColor, true);
        }
        
        
    }

    public void InitSector(Color color)
    {
        GetComponent<Image>().color = color;
        _sectColor = color;
    }
   // #endregion


    //void LateUpdate()
    //{
    //    clicked = false;
    //}
}