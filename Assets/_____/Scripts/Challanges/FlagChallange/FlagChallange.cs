using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChallange : Challange
{
    private const int FLAGS_PAIRS_COUNT = 4;

    public int PairBonus;
    private int _conusesSucceeded;
    private bool _IsFailed;

    [SerializeField] private List<FlagPair> _flagsPairs;

    private void Failed()
    {
        if (_IsFailed) return;
        _IsFailed = true;
    }

    private void OnSucceedPair()
    {
        if (_IsFailed) return;
        base.OnSucceededPart();
        _conusesSucceeded++;
        if (_conusesSucceeded == FLAGS_PAIRS_COUNT)
            OnSucceededChallange();
    }

    public override void OnSucceededChallange()
    {
    }

    public override void StartChallange()
    {

    }

    public override void Generate()
    {
        float amplitude = 0.3f;
        float frequency = 0.25f;
        float frequencyOffset = 0.25f;
        float forwardOffset = 4f;
        Vector3 spawnOffset = Vector3.zero;
        _botSpline.Initialize(FLAGS_PAIRS_COUNT + 2);
        InsertPoint(0, spawnOffset);

        for (int i = 0; i < FLAGS_PAIRS_COUNT; i++)
        {
            float xOffset = ((Mathf.Sin(i * frequency * Mathf.PI * 2f + frequencyOffset) - 0.5f) * 2f) * amplitude;
            spawnOffset = Vector3.forward * forwardOffset * (i+1) + Vector3.right * xOffset;
            FlagPair pair = Instantiate(_prefabs.FlagPair, this.transform);
            pair.transform.localPosition = spawnOffset;
            _flagsPairs.Add(pair);
            InsertPoint(i + 1, spawnOffset);

        }
        InsertPoint(FLAGS_PAIRS_COUNT + 1, _mainSpline[1].localPosition);
        _botSpline.Refresh();
        foreach (var conusPair in _flagsPairs)
        {
            conusPair.SucceedEvent += OnSucceedPair;
            conusPair.TouchedConusEvent += Failed;
        }
    }

    private void InsertPoint(int index, Vector3 localPos)
    {
        _botSpline[index].localPosition = localPos;
        _botSpline[index].localEulerAngles = Vector3.down * 90;
    }
}
