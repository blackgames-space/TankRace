using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
using System;

public class SplineLevelGeneration
{
    public List<Chunk> Chunks => _chunks;
    private List<Chunk> _chunks = new List<Chunk>();
    private const int CHUNKS_BUFFER = 20;
    private const float LINES_DISTANCE = 16f;
    private readonly Prefabs _prefabs;
    private readonly MainScreen _mainScreen;
    private readonly GameSettings _gameSettings;
    private int _chunksCount;

    public SplineLevelGeneration(
        Prefabs prefabs,
        MainScreen mainScreen,
        GameSettings gameSettings)
    {
        this._prefabs = prefabs;
        this._mainScreen = mainScreen;
        this._gameSettings = gameSettings;
    }

    public List<Chunk> BuildInitialLevel(int line)
    {
        _chunks.Clear();
        _chunksCount = 0;
        for (int i = 0; i < CHUNKS_BUFFER; i++)
        {
            SpawnSpline(line);
        }
        return new List<Chunk>(_chunks);
    }


    public void SpawnSpline(int line)
    {
        Chunk chunkPrerfab;
        if (_chunksCount % 7 == 6)
        {
            chunkPrerfab = _prefabs.WaterBridgeChunkPrefab;
        }
        else
        if (_chunksCount % 7 == 5)
        {
            chunkPrerfab = _prefabs.TurnLeftSplinePrefab[line];
        }
        else
        if (_chunksCount % 7 == 4)
        {
            chunkPrerfab = _prefabs.ConusSplinePrefab;
        }
        else
        if (_chunksCount % 7 == 3)
        {
            chunkPrerfab = _prefabs.WaterBridgeChunkPrefab;

            //chunkPrerfab = _prefabs.ShootingChunkPrefab;
        }
        else
        if (_chunksCount % 7 == 2)
        {
            chunkPrerfab = _prefabs.ObstacleChunkPrefab;
        }
        else
        if (_chunksCount % 7 == 1)
        {
            chunkPrerfab = _prefabs.FlagChunkPrefab;
        }
        else
        {
            chunkPrerfab = _prefabs.StraightSplinePrefab;
        }

        Chunk chunk = GameObject.Instantiate(chunkPrerfab);
        BezierSpline spline = chunk.Spline;

        Vector3 splinePosition;
        Quaternion splineRotation;
        if (_chunks.Count > 0)
        {
            BezierSpline lastSpline = _chunks[_chunks.Count - 1].Spline;
            splinePosition = lastSpline[lastSpline.Count - 1].position;
            splineRotation = lastSpline[lastSpline.Count - 1].rotation;
        }
        else
        {
            splinePosition = Vector3.left * LINES_DISTANCE + Vector3.right * LINES_DISTANCE * line;
            splineRotation = Quaternion.identity;
        }
        spline.transform.position = splinePosition;
        spline.transform.rotation = splineRotation;
        spline.Refresh();
        chunk.Construct(new Challange.Bindings(_gameSettings, _mainScreen, _prefabs));
        _chunks.Add(chunk);
        _chunksCount++;
    }
}
