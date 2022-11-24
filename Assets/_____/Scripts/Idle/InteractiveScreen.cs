using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractiveScreen : InteractableView
{

    internal override UnityEvent InteractedEvent => ButtonPressedEvent;

    private UnityEvent ButtonPressedEvent { get; set; } = new UnityEvent();
    [SerializeField] private Button _interactButton;

    private void OnInteracted()
    {
        Taptic.Light();
        ButtonPressedEvent.Invoke();
    }

    internal override void Show()
    {
        this.gameObject.SetActive(true);
        _interactButton.onClick.AddListener(OnInteracted);
    }

    internal override void Hide()
    {
        this.gameObject.SetActive(false);
        _interactButton.onClick.RemoveListener(OnInteracted);
    }
}
