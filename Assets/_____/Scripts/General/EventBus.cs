using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    public Action<ShootingChallange> PlayerStartedShootingChallangeEvent;
    public Action PlayerEnteredShootingModeEvent;
    public Action PlayerLeavedShootingModeEvent;
    public Action<ShootingChallange> PlayerStoppedShootingChallangeEvent;
}
