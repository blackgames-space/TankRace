using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class ConusChallange : Challange
{
    private const int CONUS_PAIRS_COUNT = 4;

    public int PairBonus;
    private int _conusesSucceeded;
    private bool _IsFailed;

    [SerializeField] private List<ConusPair> _conusPairs;

    private void Failed()
    {
        if (_IsFailed) return;
        Debug.Log("FailedChallange");
        _IsFailed = true;
    }

    private void OnSucceedPair()
    {
        if (_IsFailed) return;
        base.OnSucceededPart();
        _conusesSucceeded++;
        if (_conusesSucceeded == CONUS_PAIRS_COUNT)
            OnSucceededChallange();
    }

    public override void OnSucceededChallange()
    {
        Debug.Log("SucceededChallange");
    }

    public override void StartChallange()
    {

    }

    public override void Generate()
    {
        float amplitude = 0.3f;
        float frequency = 0.25f;
        float frequencyOffset = 0.25f;
        float forwardOffset = 4f;
        _botSpline.Initialize(CONUS_PAIRS_COUNT + 2);
        Vector3 spawnOffset = Vector3.zero;
        InsertPoint(0, spawnOffset);
        for (int i = 0; i < CONUS_PAIRS_COUNT; i++)
        {
            float xOffset = ((Mathf.Sin(i * frequency * Mathf.PI * 2f + frequencyOffset) - 0.5f) * 2f) * amplitude;
            spawnOffset = Vector3.forward * forwardOffset * (i+1) + Vector3.right * xOffset;
            ConusPair pair = Instantiate(_prefabs.ConusPair, this.transform);
            pair.transform.localPosition = spawnOffset;
            _conusPairs.Add(pair);
            InsertPoint(i + 1, spawnOffset);
        }
       
        InsertPoint(CONUS_PAIRS_COUNT + 1, _mainSpline[1].localPosition);
        _botSpline.Refresh();

        foreach (var conusPair in _conusPairs)
        {
            conusPair.SucceedEvent += OnSucceedPair;
            conusPair.TouchedConusEvent += Failed;
        }
    }

    private void InsertPoint(int index, Vector3 localPos)
    {
        _botSpline[index].localPosition = localPos;
        _botSpline[index].localEulerAngles = Vector3.down * 90;
    }

}
