using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentObserver<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action<T> TriggerEnterEvent;
    public event Action<T> TriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T component))
        {
            TriggerEnterEvent?.Invoke(component);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T carrier))
        {
            TriggerExitEvent?.Invoke(carrier);
        }
    }
}