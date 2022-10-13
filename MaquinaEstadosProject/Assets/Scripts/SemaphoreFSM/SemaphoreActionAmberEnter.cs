using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionAmberEnter")]
public class SemaphoreActionAmberEnter : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.stateTextC.text = "Estado: Ambar";

        s.amberLightC.material = s.amberMaterial;

        s.changeTimer = s.amberDuration;
    }
}
