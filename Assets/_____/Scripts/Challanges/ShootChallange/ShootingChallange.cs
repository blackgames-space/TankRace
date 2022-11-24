using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingChallange : Challange
{
    public Vector3 StoppingPoint => _stoppingPoint.position;

    [SerializeField] private Transform _stoppingPoint;
    [SerializeField] private Transform[] _fronts;

    private int _targetsHit;

    public override void Generate()
    {
        _botSpline = _mainSpline;
        
        for (int i = 0; i < _fronts.Length; i++)
        {
            ShootingTarget targetPrefab;
            if (i < 2)
                targetPrefab = _prefabs.StaticTarget;
            else
                if (UnityEngine.Random.Range(0, 2) == 0)
                targetPrefab = _prefabs.MovingTarget;
            else
                targetPrefab = _prefabs.FlyingTarget;

            SpawnTarget(i, targetPrefab);
        }

        foreach (var front in _fronts)
        {
            front.gameObject.SetActive(false);
        }
    }

    public void SpawnTarget(int front, ShootingTarget prefab)
    {
        ShootingTarget target = Instantiate(prefab, _fronts[front]);
        float h = _fronts[front].localPosition.x;
        float l = (2f * h) / Mathf.Tan((_gameSettings.ShootingRangeAngleDeg / 2f) * Mathf.Deg2Rad);
        target.SetFrontLength(l);
        target.HitEvent += OnTargetHit;
    }

    public override void OnSucceededChallange()
    {
        base.OnSucceededChallange();
    }

    public void OnTargetHit()
    {
        _targetsHit++;
        if(_targetsHit == _fronts.Length)
        {
            OnSucceededChallange();
        }
    }

    public override void StartChallange()
    {
        foreach (var front in _fronts)
        {
            front.gameObject.SetActive(true);
        }
    }
}
