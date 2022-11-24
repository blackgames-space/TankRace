using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum CharacterStateType
{
    StartIdle,
    Running,
    Shooting
}

public class TankStateMachine
{
    private State _currentState;

    private State[] _states;

    public TankStateMachine(
        TankStateMachineData data,
        GameSettings gameSettings,
        SplineLevelGeneration levelGenerator,
        SimpleTouchInput simpleTouchInput,
        EventBus eventBus,
        BlackScreen blackScreen,
        CamerasController camerasController,
        SLS.Snapshot snapshot,
        Prefabs prefabs
        )
    {
        CharacterVisual characterVisual = GameObject.Instantiate(prefabs.TankPrefabs[snapshot.PlayerTankType]);
        List<Chunk> chunks = new List<Chunk>();
        StartIdleState startState = new StartIdleState(data, levelGenerator, characterVisual, camerasController);
        RunningState runningState = new RunningState(data, characterVisual, gameSettings, simpleTouchInput, eventBus);
        ShootingState shootingState = new ShootingState(data, eventBus, gameSettings, simpleTouchInput, characterVisual, blackScreen, camerasController, snapshot, prefabs);

        _states = new State[]
        {
            startState,
            runningState,
            shootingState
        };

        foreach (var state in _states)
        {
            state.CalledForChangeStateEvent += ChangeState;
        }

    }

    public void ChangeState(int state)
    {
        if (_currentState != null && _currentState.Type != state)
        {
            _currentState.Stop();
        }
        _currentState = _states[state];

        _currentState.Start();
    }

    public void FixedTick()
    {
        _currentState?.FixedUpdate();
    }

    public void Tick()
    {
        _currentState?.Update();
    }
}

public class StartIdleState : State
{
    private CharacterVisual _characterVisual;
    private readonly TankStateMachineData _data;
    private readonly SplineLevelGeneration _levelGenerator;
    private readonly CamerasController _camerasController;
    private readonly int _line;

    public StartIdleState(
        TankStateMachineData data,
        SplineLevelGeneration levelGenerator,
        CharacterVisual characterVisual,
        CamerasController camerasController
        )
    {
        this._data = data;
        this._levelGenerator = levelGenerator;
        _characterVisual = characterVisual;
        this._camerasController = camerasController;
    }

    public override void Start()
    {
        if (_data.IsPlayer) _camerasController.ChooseCamera(_characterVisual.RunnerCamera);
        _characterVisual.Walker.spline = _data.TrackChunks[0].Spline;
        _characterVisual.Walker.transform.position = _characterVisual.Walker.spline.transform.position;
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

public class RunningState : State
{
    private readonly TankFactory _factory;
    private readonly TankStateMachineData _data;
    private CharacterVisual _characterVisual;
    private readonly GameSettings _gameSettings;
    private readonly SimpleTouchInput _simpleTouchInput;
    private readonly EventBus _eventBus;
    private float _forwardVelocity;
    private float _distance;
    private int _currentSplineIndex;
    private bool _IsRunning;
    private float _turning;
    private float _targetTurning;

    private float _currentDelta;

    public RunningState(
        TankStateMachineData data,
        CharacterVisual characterVisual,
        GameSettings gameSettings,
        SimpleTouchInput simpleTouchInput,
        EventBus eventBus)
    {
        this._gameSettings = gameSettings;
        this._simpleTouchInput = simpleTouchInput;
        this._eventBus = eventBus;
        this._data = data;
        _characterVisual = characterVisual;

        _eventBus.PlayerEnteredShootingModeEvent += OnPlayerStartedShooting;
        _eventBus.PlayerLeavedShootingModeEvent += OnPlayerStoppedShooting;
    }


    private void OnPlayerStartedShooting()
    {
        if (_data.IsPlayer) return;

        foreach (var chunk in _data.TrackChunks)
        {
            chunk.gameObject.SetActive(false);
        }
        _characterVisual.gameObject.SetActive(false);
        _IsRunning = false;
    }


    private void OnPlayerStoppedShooting()
    {
        if (_data.IsPlayer) return;

        foreach (var chunk in _data.TrackChunks)
        {
            chunk.gameObject.SetActive(true);
        }
        _characterVisual.gameObject.SetActive(true);

        _IsRunning = true;
    }

    public override void Start()
    {
        if (_data.IsPlayer) _simpleTouchInput.StartReading();
        _characterVisual.TankHitObstacleEvent += OnHitObstacle;
        _distance = _characterVisual.Walker.spline.transform.InverseTransformPoint(_characterVisual.Visual.transform.position).z;
        Debug.Log("DIstance: " + _distance);
        _forwardVelocity = 0f;
        _IsRunning = true;
    }

    private void OnHitObstacle()
    {
        _forwardVelocity = 0f;
    }

    public override void Stop()
    {
        if (_data.IsPlayer) _simpleTouchInput.StopReading();
    }

    public override void Update()
    {

    }

    private void CheckSplineGeneration()
    {
        if (_distance >= _characterVisual.Walker.spline.length)
        {
            float difference = _distance - _characterVisual.Walker.spline.length;
            _distance = difference;
            _currentSplineIndex++;
            Chunk nextChunk = _data.TrackChunks[_currentSplineIndex];
            if (_data.IsPlayer)
            {
                _characterVisual.Walker.spline = nextChunk.Spline;
            }
            else
            {
                if (nextChunk.Challange == null)
                    _characterVisual.Walker.spline = nextChunk.Spline;
                else
                    _characterVisual.Walker.spline = nextChunk.Challange.BotSpline;
            }
            float differenceNormal = difference / _characterVisual.Walker.spline.length;
            _characterVisual.Walker.NormalizedT = differenceNormal;

            if (nextChunk.Challange != null)
            {
                if (_data.IsPlayer)
                    nextChunk.Challange.StartChallange();
                if (nextChunk.Challange.Type == ChallangeType.Shooting && _data.IsPlayer)
                {
                    _eventBus.PlayerStartedShootingChallangeEvent?.Invoke(nextChunk.Challange as ShootingChallange);
                    CalledForChangeStateEvent?.Invoke((int)CharacterStateType.Shooting);
                }

            }
        }
    }

    public override void FixedUpdate()
    {
        if (!_IsRunning) return;

        _forwardVelocity = Mathf.Min(_forwardVelocity + _gameSettings.PlayerSpeedGain * Time.fixedDeltaTime, _gameSettings.PlayerMaxSpeed);
        _distance += _forwardVelocity * Time.fixedDeltaTime;
        _characterVisual.Walker.speed = _forwardVelocity;
        _characterVisual.Walker.Execute(Time.fixedDeltaTime);
        if (_data.IsPlayer)
        {
            float targetDelta = _simpleTouchInput.TickDelta.x * _gameSettings.RunningControlSensetivity;
            _targetTurning += targetDelta;
            _data.CurrentTurning = Mathf.Lerp(_data.CurrentTurning, _targetTurning, 0.1f);


            float difference = _targetTurning - _data.CurrentTurning;
            _characterVisual.SetRotation(new Vector3(0, difference * 60f, 0));
            Vector3 moveDelta = new Vector3(difference, 0, 0);
            _characterVisual.Walker.Offset += moveDelta;
        }
        CheckSplineGeneration();
    }
}

public class ShootingState : State
{
    private CharacterVisual _characterVisual;
    private readonly BlackScreen _blackScreen;
    private readonly CamerasController _camerasController;
    private readonly ShootingCamera _shootingCamera;
    private readonly SLS.Snapshot _snapshot;
    private readonly Prefabs _prefabs;
    private readonly GameSettings _gameSettings;
    private readonly SimpleTouchInput _simpleTouchInput;
    private readonly TankStateMachineData _data;
    private readonly EventBus _eventBus;
    private float _forwardVelocity;
    private float _distance;
    private int _currentSplineIndex;
    private ShootingChallange _shootingChallange;
    private float _currentLoading;
    private float _currentLoadingPercent;
    private float _loadingTime;
    private bool _IsLoaded;

    public ShootingState(
        TankStateMachineData data,
        EventBus eventBus,
        GameSettings gameSettings,
        SimpleTouchInput simpleTouchInput,
        CharacterVisual characterVisual,
        BlackScreen blackScreen,
        CamerasController camerasController,
        SLS.Snapshot snapshot,
        Prefabs prefabs)
    {
        _gameSettings = gameSettings;
        _simpleTouchInput = simpleTouchInput;
        _characterVisual = characterVisual;
        this._blackScreen = blackScreen;
        this._camerasController = camerasController;
        this._snapshot = snapshot;
        this._prefabs = prefabs;
        this._data = data;
        this._eventBus = eventBus;
        _shootingCamera = _characterVisual.ShootingCamera;
        if (_data.IsPlayer)
            _eventBus.PlayerStartedShootingChallangeEvent += OnStartedShoootingChallange;
    }

    private void OnStartedShoootingChallange(ShootingChallange challange)
    {
        _shootingChallange = challange;
        _shootingChallange.SucceedEvent += OnCompletedShooting;
    }

    private void OnCompletedShooting()
    {
        Debug.Log("Succeeded shooting: " + _data.IsPlayer);
        _characterVisual.StartCoroutine(CompletedShoootingRoutine());
    }

    private IEnumerator CompletedShoootingRoutine()
    {
        _blackScreen.FadeIn();
        yield return new WaitForSeconds(0.4f);
        _characterVisual.Cannon.gameObject.SetActive(false);
        _camerasController.ChooseCamera(_characterVisual.RunnerCamera);
        yield return new WaitForSeconds(0.2f);
        _eventBus.PlayerLeavedShootingModeEvent?.Invoke();
        _blackScreen.FadeOut();
        yield return new WaitForSeconds(0.5f);
        CalledForChangeStateEvent?.Invoke((int)CharacterStateType.Running);
    }

    public override void Start()
    {
        _loadingTime = 4f - _snapshot.LoadingLevel * 0.3f;
        _currentLoading = _loadingTime;
        _currentLoadingPercent = 1f;
        _IsLoaded = true;

        Vector3 distanceToStoppingPoint = _shootingChallange.StoppingPoint - _characterVisual.Visual.transform.position;

        Vector3 distanceToStoppingPointLocal = _characterVisual.Walker.spline.transform.InverseTransformVector(distanceToStoppingPoint);
        float moveTimeZ = distanceToStoppingPointLocal.z / _characterVisual.Walker.speed;
        _characterVisual.Visual.transform.DOMove(_shootingChallange.StoppingPoint, moveTimeZ).OnComplete(OnMovedToShootingPoint);
        _characterVisual.StartCoroutine(SetShootingView(moveTimeZ));
    }

    private void OnEndedHolding()
    {
        if (_IsLoaded)
        {
            Shoot();
            _IsLoaded = false;
            _currentLoading = 0f;
            _currentLoadingPercent = 0f;
        }
    }

    private void Shoot()
    {
        ShootingShell shell = GameObject.Instantiate(_prefabs.TankShellPrefab);
        shell.transform.position = _characterVisual.CannonTip.position;
        shell.transform.rotation = _camerasController.CurrentCamera.transform.rotation;
        shell.Shoot(_gameSettings.ShellInitialSpeed);
    }

    private IEnumerator SetShootingView(float moveTime)
    {
        yield return new WaitForSeconds(moveTime - 0.5f);
        _camerasController.ChooseCamera(_characterVisual.ShootingProxyCamera);
        yield return new WaitForSeconds(0.5f);
        _blackScreen.FadeIn();
        yield return new WaitForSeconds(0.3f);
        _camerasController.ChooseCamera(_characterVisual.ShootingCamera.Camera);
        _characterVisual.Cannon.gameObject.SetActive(true);
        _eventBus.PlayerEnteredShootingModeEvent?.Invoke();
        yield return new WaitForSeconds(0.2f);
        _blackScreen.FadeOut();
        _simpleTouchInput.StartReading();
        _simpleTouchInput.EndedHoldingEvent += OnEndedHolding;
    }

    private void OnMovedToShootingPoint()
    {

    }

    public override void Stop()
    {
        if (_data.IsPlayer) _simpleTouchInput.StopReading();
        _characterVisual.Walker.SetWorldPositionToSplinePos(_characterVisual.Visual.transform.position);
        _eventBus.PlayerStoppedShootingChallangeEvent?.Invoke(null);
    }

    public override void Update()
    {
        ControllingCamera();
        Loading();
    }

    private void ControllingCamera()
    {
        Vector3 rotation = new Vector3(-_simpleTouchInput.TickDelta.y, _simpleTouchInput.TickDelta.x) * _gameSettings.ShootingCameraSens;
        _shootingCamera.Rotate(rotation);
    }

    private void Loading()
    {
        if (_currentLoadingPercent < 1f)
        {
            _currentLoading += Time.deltaTime;
            _currentLoading = Mathf.Min(_loadingTime, _currentLoading);
            _currentLoadingPercent = _currentLoading / _loadingTime;
            if (_currentLoadingPercent == 1f)
            {
                _IsLoaded = true;
            }
        }
    }

    public override void FixedUpdate()
    {
        if (_data.IsPlayer)
        {
            _data.CurrentTurning = Mathf.Lerp(_data.CurrentTurning, 0f, 0.1f);

            float difference = 0f - _data.CurrentTurning;
            _characterVisual.SetRotation(new Vector3(0, difference * 60f, 0));
        }
    }
}

public abstract class State
{
    public Action<int> CalledForChangeStateEvent;
    public int Type;
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Stop();
}

