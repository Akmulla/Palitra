using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    Color sect_color;
    static float prev_touch = 0.0f;
    
    void Start()
    {
        sect_color = GetComponent<Image>().color;
    }
    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((GameController.game_controller.GetState() == GameState.Game) &&
            (Time.time - prev_touch > 0.15f))
        {
            prev_touch = Time.time;
            Ball.ball.SetColor(sect_color, true);
        }
           
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((GameController.game_controller.GetState() == GameState.Game) && 
            (Time.time - prev_touch > 0.15f))
        {
            prev_touch = Time.time;
            Ball.ball.SetColor(sect_color, false);
        }
           
    }

    public void InitSector(Color color)
    {
        GetComponent<Image>().color = color;
        sect_color = color;
    }
    #endregion
}