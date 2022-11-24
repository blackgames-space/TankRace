using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private CanvasFadeTweenAnim _canvasFadeZero;
    [SerializeField] private CanvasFadeTweenAnim _canvasFadeOne;

    internal void Show()
    {
        _canvasFadeOne.Play();
    }

    internal void Hide()
    {
        _canvasFadeZero.Play();
    }
}
