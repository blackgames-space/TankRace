using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TankFactory
{
    public Action<CharacterVisual> InstantiatedTankEvent;
    private Prefabs _prefabs;
    public CharacterVisual Character;

    public TankFactory(Prefabs prefabs, DiContainer container)
    {
        _prefabs = prefabs;
    }

}
