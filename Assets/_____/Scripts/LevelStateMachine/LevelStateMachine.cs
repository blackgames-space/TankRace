using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum LevelStateType
{
    Start,
    Main,
    Win,
    Loose
}

public class LevelStateMachine : ITickable, IInitializable, IFixedTickable
{
    private LevelState _currentState;

    private LevelState[] _states;

    public LevelStateMachine(
        StartScreen startScreen,
        EventBus eventBus,
        CamerasController camerasController,
        GameSettings gameSettings,
        SplineLevelGeneration levelGeneration,
        SimpleTouchInput simpleTouchInput,
        BlackScreen blackScreen,
        SLS.Snapshot snapshot,
        Prefabs prefabs
        )
    {

        List<TankStateMachine> tanks = new List<TankStateMachine>();

        for (int i = 0; i < 3; i++)
        {
            TankStateMachineData data = new TankStateMachineData();
            data.IsPlayer = (i == 1);
            data.Line = i;
            data.TrackChunks = levelGeneration.BuildInitialLevel(data.Line);
            TankStateMachine tank = new TankStateMachine(
              data,
                gameSettings,
                levelGeneration,
                simpleTouchInput,
                eventBus,
                blackScreen,
                camerasController,
                snapshot,
                prefabs);

            tanks.Add(tank);
        }

        StartLevelState startLevelState = new StartLevelState(tanks, startScreen);
        MainLevelState mainLevelState = new MainLevelState(tanks, eventBus, camerasController);
        WinLevelState winLevelState = new WinLevelState();
        LooseLevelState looseLevelState = new LooseLevelState();

        _states = new LevelState[]
        {
            startLevelState,
            mainLevelState,
            winLevelState,
            looseLevelState
        };

        foreach (var state in _states)
        {
            state.CalledForStateChangeEvent += ChangeState;
        }
    }

    public void ChangeState(LevelStateType stateType)
    {
        if (_currentState != null && _currentState.Type != stateType)
        {
            _currentState.Stop();
        }
        _currentState = _states[(int)stateType];

        _currentState.Start();
    }

    public void FixedTick()
    {
        _currentState?.FixedUpdate();
    }

    public void Initialize()
    {
        ChangeState(LevelStateType.Start);
    }

    public void Tick()
    {
        _currentState?.Update();
    }
}

public abstract class LevelState
{
    public Action<LevelStateType> CalledForStateChangeEvent;
    public abstract LevelStateType Type { get; }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Stop();
}

public class StartLevelState : LevelState
{
    private readonly TankStateMachine _characterStateMachine;
    private readonly List<TankStateMachine> _tanks;
    private readonly StartScreen _startScreen;

    public override LevelStateType Type => LevelStateType.Start;

    public StartLevelState(
        List<TankStateMachine> tanks,
        StartScreen startScreen)
    {
        this._tanks = tanks;
        this._startScreen = startScreen;
    }

    public override void Start()
    {
        // Skip start screen
        foreach (var tank in _tanks)
        {
            tank.ChangeState((int)CharacterStateType.StartIdle);
        }
        _startScreen.TappedEvent.AddListener(OnTappedStartScreen);
    }

    public override void Stop()
    {
        _startScreen.Hide();
    }

    public override void Update()
    {

    }

    private void OnTappedStartScreen()
    {
        CalledForStateChangeEvent?.Invoke(LevelStateType.Main);
    }

    public override void FixedUpdate()
    {

    }
}

public class MainLevelState : LevelState
{
    private readonly List<TankStateMachine> _tanks;
    private readonly EventBus _eventBus;
    private readonly CamerasController _camerasController;

    public override LevelStateType Type => LevelStateType.Main;

    public MainLevelState(
        List<TankStateMachine> tanks,
        EventBus eventBus,
        CamerasController camerasController)
    {
        this._tanks = tanks;
        this._eventBus = eventBus;
        this._camerasController = camerasController;

    }

    public override void Start()
    {
        foreach (var tank in _tanks)
        {
            tank.ChangeState((int)CharacterStateType.Running);
        }
    }

    public override void Stop()
    {

    }

    public override void Update()
    {
        foreach (var tank in _tanks)
        {
            tank.Tick();
        }
    }

    public override void FixedUpdate()
    {
        foreach (var tank in _tanks)
        {
            tank.FixedTick();
        }
    }
}

public class WinLevelState : LevelState
{
    public override LevelStateType Type => LevelStateType.Win;

    public WinLevelState()
    {

    }

    public override void Start()
    {

    }

    public override void Stop()
    {

    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}

public class LooseLevelState : LevelState
{
    public override LevelStateType Type => LevelStateType.Loose;

    public LooseLevelState()
    {

    }

    public override void Start()
    {

    }

    public override void Stop()
    {

    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}