using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMenu_Change : MonoBehaviour
{
    public SkinChange skin_change;
    public TrianChange trian_change;
    public Button_SwitchSkinMenuMode mode;

    public void SetItem()
    {
        if (mode.State == SkinState.Round)
        {
            skin_change.SetSkin();
        }
        else
        {
            trian_change.SetTrian();
        }
    }

	public void NextItem()
    {
        if (mode.State==SkinState.Round)
        {
            skin_change.NextSkin();
        }
        else
        {
            trian_change.NextTrian();
        }
    }

    public void PrevItem()
    {
        if (mode.State == SkinState.Round)
        {
            skin_change.PrevSkin();
        }
        else
        {
            trian_change.PrevTrian();
        }
    }
}
