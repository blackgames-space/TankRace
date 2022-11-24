using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBridgeChallange : Challange
{
    private const int SQUARES_COUNT = 4;
    private const int SQUARES_ROW_COUNT = 3;
    public override void Generate()
    {
        //float frequency = 0.25f;
        //float frequencyOffset = 0.25f;
        //float squareSize = 1f;
        //_botSpline.Initialize(SQUARES_COUNT + 2);
        //Vector3 spawnOffset = Vector3.zero;
        //InsertPoint(0, spawnOffset);
        //float prevSin = 0f;
        //spawnOffset += Vector3.forward * squareSize * 0.5f;
        //int nPrev = 1;
        //for (int i = 0; i < SQUARES_COUNT; i++)
        //{
        //    float sin = ((Mathf.Sin(i * frequency * Mathf.PI * 2f + frequencyOffset) - 0.5f) * 2f);
        //    int n = Mathf.CeilToInt(sin * (SQUARES_ROW_COUNT / 2)) + SQUARES_ROW_COUNT / 2;

        //    GameObject square = null;
        //    for (int j = 0; j < SQUARES_ROW_COUNT; j++)
        //    {
        //        if (j == n || j == nPrev)
        //        {
        //            //square = bridge
        //        }
        //        else
        //        {
        //            //square = water;
        //        }
        //        square.transform.localPosition = spawnOffset + Vector3.left * squareSize * 1.5f + Vector3.right * squareSize * j;
        //    }
        //    spawnOffset += Vector3.forward * squareSize;
        //    Vector3 prevNPos = spawnOffset + Vector3.left * squareSize * 1.5f + Vector3.right * squareSize * nPrev;
        //    Vector3 nPos = spawnOffset + Vector3.left * squareSize * 1.5f + Vector3.right * squareSize * n;
        //    Vector3 midN = (prevNPos + nPos) * 0.5f;
        //    nPrev = n;


        //    InsertPoint(i + 1, midN);

        //    //spawnOffset += Vector3.forward * squareSize;

        //    //if (Mathf.Sign(prevSin) == Mathf.Sign(sin))
        //    //{
        //    //    if (sin > 0)
        //    //    {
        //    //        n++;
        //    //    }
        //    //    else
        //    //    {
        //    //        n--;
        //    //    }

        //    //    n 
        //    //}
        //    //prevSin = sin;

        //}

        //InsertPoint(SQUARES_COUNT + 1, _mainSpline[1].localPosition);
        //_botSpline.Refresh();
        _botSpline = _mainSpline;
    }

    private void InsertPoint(int index, Vector3 localPos)
    {
        _botSpline[index].localPosition = localPos;
        _botSpline[index].localEulerAngles = Vector3.down * 90;
    }

    public override void StartChallange()
    {

    }
}
