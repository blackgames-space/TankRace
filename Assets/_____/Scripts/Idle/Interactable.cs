using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    internal UnityEvent InteractedEvent { get; private set; } = new UnityEvent();
    protected abstract InteractableView InteractableView { get; }

    internal bool IsChosen => _IsChosen;
    protected bool _IsChosen;

    internal void Choose()
    {
        OnChosenScreen();
        _IsChosen = true;
        InteractableView.Show();
        InteractableView.InteractedEvent.AddListener(InteractedEvent.Invoke);
    }
    internal void Unchoose()
    {
        OnUnchosenScreen();
        _IsChosen = false;
        InteractableView.Hide();
        InteractableView.InteractedEvent.RemoveListener(InteractedEvent.Invoke);
    }

    protected abstract void OnChosenScreen();
    protected abstract void OnUnchosenScreen();

    private void OnTriggerEnter(Collider other)
    {
        PlayerInteraction.Instance.EnterScreenTrigger(this);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInteraction.Instance.ExitScreenTrigger(this);
    }
}
