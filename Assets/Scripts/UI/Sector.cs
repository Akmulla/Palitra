using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public class Sector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, ISelectHandler,
//IPointerDownHandler
public class Sector : Selectable, IPointerClickHandler
{
    Color sect_color;
    //static float prev_touch = 0.0f;
   // bool clicked = false;
    
    void Start()
    {
        sect_color = GetComponent<Image>().color;
    }
    //#region IPointerClickHandler implementation

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("sdg");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
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
#if UNITY_EDITOR
        Ball.ball.SetColor(sect_color, true);
#endif
        //    //print("click");
        //}

    }

    void Update()
    {
        if (GameController.game_controller == null)
            return;
        if (GameController.game_controller.GetState() != GameState.Game)
            interactable = false;
        else
            interactable = true;
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if ((GameController.game_controller.GetState() == GameState.Game)
            //&& (Time.time - prev_touch > 0.15f)
            //&& (!clicked)
        )
        {
            //  prev_touch = Time.time;
            // clicked = true;
            Ball.ball.SetColor(sect_color, true);
            this.DoStateTransition(SelectionState.Highlighted, true);
        }


    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if ((GameController.game_controller.GetState() == GameState.Game)
            //&& (Time.time - prev_touch > 0.15f)
            //&& (!clicked)
        )
        {
            //  prev_touch = Time.time;
            // clicked = true;
            //Ball.ball.SetColor(sect_color, true);
            this.DoStateTransition(SelectionState.Normal, true);
        }

    }

    public void InitSector(Color color)
    {
        GetComponent<Image>().color = color;
        sect_color = color;
    }
   // #endregion


    //void LateUpdate()
    //{
    //    clicked = false;
    //}
}