using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionRedExit")]
public class SemaphoreActionRedExit : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.redLightC.material = s.offMaterial;

        s.changeTimer = s.redDuration;
    }
}
