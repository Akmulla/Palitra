using UnityEngine;

public class BuyHeartMenu : MonoBehaviour
{
    public int hearts;
    public int price;

	public void BuyHeart()
    {
        if (GlobalScore.globalScore.Score >= price)
        {
            GlobalScore.globalScore.Score -= price;
            Hearts.h.Heart += hearts;
        }
    }
}
