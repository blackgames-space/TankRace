using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Wallet : MonoBehaviour
{
    [SerializeField] private bool _IsOnLevel;
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _redGlowParticles;

    private Money _money;

    [Inject]
    private void Construct(Money money)
    {
        _money = money;
        SetMoney(money.Count);
        if (_IsOnLevel)
            _money.MoneyOnLevelUpdatedEvent.AddListener(SetMoney);
        else
            _money.MoneyUpdatedEvent.AddListener(SetMoney);

        _money.MoneyNotEnoughEvent.AddListener(BlinkRed);
    }

    public void SetMoney(int count)
    {
        _textLabel.text = AbbrevationUtility.AbbreviateNumber(count);
        _animator.Play("Update");
    }

    internal void BlinkRed()
    {
        _animator.Play("UpdateEmpty");
        _redGlowParticles.Play();
    }
}
