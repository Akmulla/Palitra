using UnityEngine;

public class SkinMenuChange : MonoBehaviour
{
    public SkinChange skinChange;
    public TrianChange trianChange;
    public ButtonSwitchSkinMenuMode mode;

    public void SetItem()
    {
        if (mode.State == SkinState.Round)
        {
            skinChange.SetSkin();
        }
        else
        {
            trianChange.SetTrian();
        }
    }

	public void NextItem()
    {
        if (mode.State==SkinState.Round)
        {
            skinChange.NextSkin();
        }
        else
        {
            trianChange.NextTrian();
        }
    }

    public void PrevItem()
    {
        if (mode.State == SkinState.Round)
        {
            skinChange.PrevSkin();
        }
        else
        {
            trianChange.PrevTrian();
        }
    }
}
