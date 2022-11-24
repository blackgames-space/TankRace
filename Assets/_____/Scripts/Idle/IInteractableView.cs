using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableView : MonoBehaviour
{
    internal abstract UnityEvent InteractedEvent { get; }
    internal abstract void Show();
    internal abstract void Hide();
}
