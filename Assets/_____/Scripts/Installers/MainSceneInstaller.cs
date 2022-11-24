using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Money>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelStateMachine>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SplineLevelGeneration>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SimpleTouchInput>().AsSingle().NonLazy();
        Container.Bind<EventBus>().AsSingle().NonLazy();
    }
}
