using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitializableComponent : MonoBehaviour
{
    internal abstract void Load();
    internal abstract void Initialize();
    internal abstract void Setup();
}
