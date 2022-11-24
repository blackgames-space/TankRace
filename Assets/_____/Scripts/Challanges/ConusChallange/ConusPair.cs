using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConusPair: MonoBehaviour
{

    public Action TouchedConusEvent;
    public Action SucceedEvent;
    [SerializeField] public PlayerTankTriggerObserver[] _conusTouchObservers;
    [SerializeField] public PlayerTankTriggerObserver _centerObserver;

    private bool _IsTouchedConus;

    private void Start()
    {
        _centerObserver.TriggerExitEvent += OnTankExited;
        foreach (var conus in _conusTouchObservers)
        {
            conus.TriggerEnterEvent += OnTankTouchedConus;
        }
    }

    private void OnTankTouchedConus(PlayerTankTrigger tank)
    {
        _IsTouchedConus = true;
        TouchedConusEvent?.Invoke();
    }

    private void OnTankExited(PlayerTankTrigger tank)
    {
        if (_IsTouchedConus) return;

        SucceedEvent?.Invoke();
    }
}