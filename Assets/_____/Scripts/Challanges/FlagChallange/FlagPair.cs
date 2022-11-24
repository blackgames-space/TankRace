using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPair : MonoBehaviour
{
    public Action TouchedConusEvent;
    public Action SucceedEvent;
    [SerializeField] public PlayerTankTriggerObserver[] _flagTouchObservers;
    [SerializeField] public PlayerTankTriggerObserver _centerObserver;

    private bool _IsTouchedFlag;

    private void Start()
    {
        _centerObserver.TriggerExitEvent += OnTankExited;
        foreach (var flag in _flagTouchObservers)
        {
            flag.TriggerEnterEvent += OnTankTouchedFlag;
        }
    }

    private void OnTankTouchedFlag(PlayerTankTrigger tank)
    {
        _IsTouchedFlag = true;
        TouchedConusEvent?.Invoke();
    }

    private void OnTankExited(PlayerTankTrigger tank)
    {
        if (_IsTouchedFlag) return;

        SucceedEvent?.Invoke();
    }
}
