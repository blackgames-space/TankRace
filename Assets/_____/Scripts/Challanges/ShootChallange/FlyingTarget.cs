using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingTarget : ShootingTarget
{
    Sequence _seq;
    float _timing;
    protected override void Start()
    {
        base.Start();

        //_timing = 1f;
        //float height = 2f;
        //float length = 2f;
        //this.transform.localPosition += Vector3.up * 2f;
        //_seq = DOTween.Sequence();

        //_seq.Append(this.transform.DOBlendableLocalMoveBy(Vector3.up * height, _timing ).SetSpeedBased().SetEase(Ease.InCirc));
        //_seq.Join(this.transform.DOBlendableLocalMoveBy(Vector3.right * length, _timing).SetSpeedBased().SetEase(Ease.OutCirc));

        //_seq.Append(this.transform.DOBlendableLocalMoveBy(Vector3.up * height, _timing).SetSpeedBased().SetEase(Ease.OutCirc));
        //_seq.Join(this.transform.DOBlendableLocalMoveBy(Vector3.left * length, _timing).SetSpeedBased().SetEase(Ease.InCirc));

        //_seq.Append(this.transform.DOBlendableLocalMoveBy(Vector3.down * height, _timing).SetSpeedBased().SetEase(Ease.InCirc));
        //_seq.Join(this.transform.DOBlendableLocalMoveBy(Vector3.left * length, _timing).SetSpeedBased().SetEase(Ease.OutCirc));

        //_seq.Append(this.transform.DOBlendableLocalMoveBy(Vector3.down * height, _timing).SetSpeedBased().SetEase(Ease.OutCirc));
        //_seq.Join(this.transform.DOBlendableLocalMoveBy(Vector3.right * length, _timing).SetSpeedBased().SetEase(Ease.InCirc));

        //_seq.SetLoops(-1);

    }
}
