using BezierSolution;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Prefabs 
{
    public Chunk StraightSplinePrefab;
    public Chunk [] TurnLeftSplinePrefab;
    public Chunk ConusSplinePrefab;
    public Chunk ShootingChunkPrefab;
    public Chunk ObstacleChunkPrefab;
    public Chunk FlagChunkPrefab;
    public Chunk WaterBridgeChunkPrefab;
    public ConusPair ConusPair;
    public FlagPair FlagPair;
    public ObstaclesPair ObstaclesPair;
    public ShootingShell TankShellPrefab;
    public ShootingTarget StaticTarget;
    public ShootingTarget MovingTarget;
    public ShootingTarget FlyingTarget;
    public BezierPoint BezierPoint;
    public GameObject WaterSquare;
    public GameObject BridgeSquare;

    public CharacterVisual[] TankPrefabs;
}
