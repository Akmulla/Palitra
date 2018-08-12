using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDefaultLines : Pool
{
    int col = 0;
    int prev_col = 0;
    int ban_col=4;

    int create_col=0;

    public override void Awake()
    {
        create_col = 0;
        prev_col = 0;
        col = 0;
        base.Awake();
    }
    public override GameObject Pop()
    {
        ban_col = 4;
        List<int> ind = new List<int>();
        for (int i = 0; i < stck.Length; i++)
        {
            if ((!stck[i].activeSelf) && (i/3!=ban_col))
                ind.Add(i);
        }

        int i_col = ind[Random.Range(0, ind.Count)];
        col = i_col / 3;

        if (col == prev_col)
            ban_col = i_col;

        prev_col = i_col;
        return stck[i_col];
    }

    public override IEnumerator InitLineCor(int k)
    {
        while (stck[k].activeSelf)
            yield return new WaitForEndOfFrame();

        stck[k].GetComponent<Line_Default>().
            InitLine(SkinManager.skin_manager.GetCurrentSkin().colors[create_col / 3] );
        create_col++;
        if (create_col >= 9)
            create_col = 0;
        yield return null;
    }
}
