using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    public Action HitEvent;
    [SerializeField] private ShellObserver _shellOberver;
    protected float _frontLength;

    protected virtual void Start()
    {
        _shellOberver.TriggerEnterEvent += OnShellEntered;
    }

    private void OnShellEntered(ShootingShell shell)
    {
        shell.OnHitTarget();
        HitEvent?.Invoke();
        Destroy(gameObject);
    }

    public void SetFrontLength(float length)
    {
        _frontLength = length;
        this.transform.localPosition = new Vector3(UnityEngine.Random.Range((-length+2)/2f, (length-2) / 2f), 0, 0);
    }
}
