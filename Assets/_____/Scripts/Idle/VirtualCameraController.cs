using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Zenject;
using System;

public class VirtualCameraController : MonoBehaviour, IInitializable
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public void Initialize()
    {
        _virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
    }

    internal void SetTarget(Transform transform)
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;
    }

    internal void IncrPriority()
    {
        _virtualCamera.Priority = 11;
    }

    internal void DecPriority()
    {
        _virtualCamera.Priority = 10;
    }
}
