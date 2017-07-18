using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    Color sect_color;
    
    void Start()
    {
        sect_color = GetComponent<Image>().color;
    }

    //void OnEnable()
    //{
    //    EventManager.StartListening("ChangeLvl", ChangeLevel);
    //}

    //void OnDisable()
    //{
    //    EventManager.StopListening("ChangeLvl", ChangeLevel);
    //}

    //void ChangeLevel()
    //{

    //}
    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        //print("Clicked me!"+gameObject.name);
        if (GameController.game_controller.GetState()==GameState.Game)
            Ball.ball.SetColor(sect_color,true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Clicked me!"+gameObject.name);
        if (GameController.game_controller.GetState() == GameState.Game)
            Ball.ball.SetColor(sect_color,false);
    }

    public void InitSector(Color color)
    {
        GetComponent<Image>().color = color;
        sect_color = color;
    }
    #endregion
}