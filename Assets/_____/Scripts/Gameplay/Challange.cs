using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public enum ChallangeType
{
    None,
    Conus,
    Shooting,
    Obstacle,
    Flag,
    WaterBridge
}

public abstract class Challange : MonoBehaviour
{
    public Action SucceedEvent;
    public ChallangeType Type;
    public BezierSpline BotSpline => _botSpline;
    [SerializeField] private int Cost;
    [SerializeField] protected BezierSpline _botSpline;
    protected BezierSpline _mainSpline;
    protected MainScreen _mainScreen;
    protected Prefabs _prefabs;
    protected GameSettings _gameSettings;

    internal void Construct(Bindings bindings, BezierSpline mainSpline)
    {
        _mainScreen = bindings.MainScreen;
        _prefabs = bindings.Prefabs;
        _gameSettings = bindings.GameSettings;
        _mainSpline = mainSpline;
    }

    public abstract void StartChallange();

    public abstract void Generate();
    public virtual void OnSucceededChallange()
    {
        SucceedEvent?.Invoke();
    }


    public void OnSucceededPart()
    {

    }

    public class Bindings
    {
        public Bindings(
            GameSettings gameSettings,
            MainScreen mainScreen,
            Prefabs prefabs)
        {
            GameSettings = gameSettings;
            MainScreen = mainScreen;
            Prefabs = prefabs;
        }

        public GameSettings GameSettings { get; }
        public MainScreen MainScreen { get; }
        public Prefabs Prefabs { get; }
    }
}

