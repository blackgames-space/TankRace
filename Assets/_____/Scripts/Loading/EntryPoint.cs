using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntryPoint : IInitializable
{
    private SceneLoaderWrapper _loader;

    public EntryPoint(SceneLoaderWrapper loader)
    {
        _loader = loader;
    }

    public void Initialize()
    {
        _loader.LoadLevel(1);
    }
}
