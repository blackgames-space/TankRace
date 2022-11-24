using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Action TankHitEvent;
    [SerializeField] private PlayerTankTriggerObserver _tankObserver;

    private void Awake()
    {
        _tankObserver.TriggerEnterEvent += OnTankEntered;
    }

    private void OnTankEntered(PlayerTankTrigger tank)
    {
        TankHitEvent?.Invoke();
        tank.Component.OnHitObstacle();
        Destroy(gameObject);
    }
}
