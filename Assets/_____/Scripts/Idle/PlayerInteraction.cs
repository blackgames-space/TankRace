using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : InitializableComponent
{
    private Interactable _chosen;
    internal static PlayerInteraction Instance;

    private List<Interactable> _interactablesInTrigger = new List<Interactable>();
    private bool _IsActive;

    internal void EnterScreenTrigger(Interactable interactable)
    {
        if (_interactablesInTrigger.Count == 0)
        {
            interactable.Choose();
            _chosen = interactable;
        }
        _interactablesInTrigger.Add(interactable);
    }

    internal void ExitScreenTrigger(Interactable interactable)
    {
        _interactablesInTrigger.Remove(interactable);
        if (interactable == _chosen)
        {
            interactable.Unchoose();
            _chosen = null;
        }
    }

    private void Choose(Interactable itemToChoose)
    {

        _chosen = itemToChoose;
        foreach (var item in _interactablesInTrigger)
        {
            if (item != itemToChoose && item.IsChosen)
            {
                item.Unchoose();
            }
        }
        itemToChoose.Choose();
    }

    internal override void Initialize()
    {
        Instance = this;
    }

    internal override void Load()
    {

    }

    internal override void Setup()
    {
        Activate();
    }

    internal void Activate()
    {
        _IsActive = true;
    }

    internal void Deactivate()
    {
        _IsActive = false;
        if (_chosen != null)
            _chosen.Unchoose();
    }

    private void Update()
    {
        if (!_IsActive) return;

        ChooseClosest();

    }

    internal void ChooseClosest()
    {
        if (_interactablesInTrigger.Count > 0)
        {
            float minDist = Vector3.Distance(_interactablesInTrigger[0].transform.position, this.transform.position);
            Interactable closest = _interactablesInTrigger[0];
            foreach (var item in _interactablesInTrigger)
            {
                float dist = Vector3.Distance(item.transform.position, this.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = item;
                }
            }
            if (!closest.IsChosen)
            {
                Choose(closest);
            }
        }
    }
}
