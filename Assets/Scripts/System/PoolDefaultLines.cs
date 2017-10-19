using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDefaultLines : Pool
{
    int _col;
    int _prevCol;
    int _banCol=4;

    int _createCol;
    //GameObject[,] lines = new GameObject[3, 3];

    public override void Awake()
    {
        _createCol = 0;
        _prevCol = 0;
        _col = 0;
        base.Awake();
    }
    public override GameObject Pop()
    {
        _banCol = 4;
        List<int> ind = new List<int>();
        for (int i = 0; i < stck.Length; i++)
        {
            if ((!stck[i].activeSelf)&&(i/3!=_banCol))
                ind.Add(i);
        }


        int iCol = ind[Random.Range(0, ind.Count)];
        _col = iCol / 3;

        if (_col == _prevCol)
            _banCol = iCol;

        _prevCol = iCol;
        return stck[iCol];
    }

    public override IEnumerator InitLineCor(int k)
    {
        while (stck[k].activeSelf)
            yield return new WaitForEndOfFrame();

        stck[k].GetComponent<LineDefault>().
            InitLine(SkinManager.skinManager.GetCurrentSkin().colors[_createCol / 3] );
        _createCol++;
        if (_createCol >= 9)
            _createCol = 0;
        yield return null;
    }
}
