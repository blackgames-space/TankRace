using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class Chunk : MonoBehaviour
{
    public Action<Challange> EnteredChallangeEvent;

    public BezierSpline Spline => _spline;
    public Challange Challange => _challange;

    [SerializeField] private Challange _challange;
    [SerializeField] private BezierSpline _spline;

    public void Construct(Challange.Bindings challlangeBindings)
    {
        if (_challange != null)
        {
            _challange.Construct(challlangeBindings, _spline);
            
            GenerateChallange();
        }

    }

    public void GenerateChallange()
    {
        _challange.Generate();
    }

    public void EnterChunk()
    {
        if (_challange != null)
        {
            EnteredChallangeEvent?.Invoke(_challange);
        }
    }
}
