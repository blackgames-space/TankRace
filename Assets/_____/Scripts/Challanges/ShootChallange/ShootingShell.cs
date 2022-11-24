using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingShell : MonoBehaviour
{
    [SerializeField] private GameObject _shellVisual;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    internal void Shoot(float speed)
    {
        _rigidbody.velocity =this.transform.forward * speed;
    }

    internal void OnHitTarget()
    {
        _rigidbody.isKinematic = true;
        _shellVisual.SetActive(false);
    }
}
