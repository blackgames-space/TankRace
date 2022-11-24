using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChallange : Challange
{
    private const int OBSTACLE_PAIRS_COUNT = 4;

    public Action HitEvent;

    [SerializeField] private List<ObstaclesPair> _obstaclesPairs;


    public override void Generate()
    {
        float amplitude = 0.3f;
        float frequency = 0.25f;
        float frequencyOffset = 0.25f;
        float forwardOffset = 4f;
        _botSpline.Initialize(OBSTACLE_PAIRS_COUNT + 2);
        Vector3 spawnOffset = Vector3.zero;
        InsertPoint(0, spawnOffset);
        for (int i = 0; i < OBSTACLE_PAIRS_COUNT; i++)
        {
            float xOffset = ((Mathf.Sin(i * frequency * Mathf.PI * 2f + frequencyOffset) - 0.5f) * 2f) * amplitude;
            spawnOffset = Vector3.forward * forwardOffset * (i+1) + Vector3.right * xOffset;
            ObstaclesPair pair = Instantiate(_prefabs.ObstaclesPair, this.transform);
            pair.transform.localPosition = spawnOffset;
            _obstaclesPairs.Add(pair);
            InsertPoint(i + 1, spawnOffset);

        }

        InsertPoint(OBSTACLE_PAIRS_COUNT + 1, _mainSpline[1].localPosition);
        _botSpline.Refresh();

        foreach (var obstaclePair in _obstaclesPairs)
        {
            obstaclePair.TankHitEvent += HitEvent;
        }
    }

    public override void StartChallange()
    {

    }

    private void InsertPoint(int index, Vector3 localPos)
    {
        _botSpline[index].localPosition = localPos;
        _botSpline[index].localEulerAngles = Vector3.down * 90;
    }
}
