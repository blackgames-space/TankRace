using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    internal UnityEvent TappedEvent => _touchDownButton.TouchDownEvent;

    [SerializeField] private TouchDownButton _touchDownButton;

    internal void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
