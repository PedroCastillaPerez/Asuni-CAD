using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionAmberExit")]
public class SemaphoreActionAmberExit : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.amberLightC.material = s.offMaterial;
    }
}
