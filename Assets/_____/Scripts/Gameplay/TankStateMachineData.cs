using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStateMachineData
{
    public bool IsPlayer;
    public int Line;
    public List<Chunk> TrackChunks;
    public float CurrentTurning;
}
