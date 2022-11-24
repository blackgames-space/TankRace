using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Money>().AsSingle().NonLazy();
        Container.Bind<SLS.Snapshot>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ComplexTouchInput.ComplexTouchInput>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EventLogger>().AsSingle().NonLazy();
    }
}