using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionGreenEnter")]
public class SemaphoreActionGreenEnter : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.stateTextC.text = "Estado: Verde";

        s.greenLightC.material = s.greenMaterial;

        s.changeTimer = s.greenDuration;

    }
}
