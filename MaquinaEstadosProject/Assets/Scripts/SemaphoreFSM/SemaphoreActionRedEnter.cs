using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionRedEnter")]
public class SemaphoreActionRedEnter : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.stateTextC.text = "Estado: Rojo";

        s.redLightC.material = s.redMaterial;

        s.changeTimer = s.redDuration;
    }
}
