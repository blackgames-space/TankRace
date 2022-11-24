using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private FadeTweenAnim _fadeIn;
    [SerializeField] private FadeTweenAnim _fadeOut;

    public void FadeIn()
    {
        _fadeIn.Play();
    }

    public void FadeOut()
    {
        _fadeOut.Play();
    }
}
