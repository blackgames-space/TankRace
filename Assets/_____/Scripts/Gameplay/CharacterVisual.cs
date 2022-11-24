using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class CharacterVisual : MonoBehaviour
{
    public Action TankHitObstacleEvent;

    public Transform Visual => _visual;
    public Transform Cannon => _cannon;
    public Transform CannonTip => _cannonTip;
    public ShootingCamera ShootingCamera => _shootingCamera;
    public VirtualCameraController ShootingProxyCamera => _shoootingProxyCamera;
    public VirtualCameraController RunnerCamera => _runnerCamera;
    public BezierWalkerWithSpeed Walker => _walker;

    [SerializeField] private Transform _visual;
    [SerializeField] private Transform _cannon;
    [SerializeField] private Transform _cannonTip;
    [SerializeField] private ShootingCamera _shootingCamera;
    [SerializeField] private VirtualCameraController _runnerCamera;
    [SerializeField] private VirtualCameraController _shoootingProxyCamera;

    [SerializeField] private BezierWalkerWithSpeed _walker;

    public void OnHitObstacle()
    {
        TankHitObstacleEvent?.Invoke();
    }

    public void SetRotation(Vector3 eulers)
    {
        Visual.transform.localEulerAngles = eulers;
    }

}
