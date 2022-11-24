using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPair : MonoBehaviour
{
    public Action TankHitEvent;

    [SerializeField] private Obstacle _obstacle1;
    [SerializeField] private Obstacle _obstacle2;

    private void Awake()
    {
        _obstacle1.TankHitEvent += TankHitEvent;
        _obstacle2.TankHitEvent += TankHitEvent;
    }
}
