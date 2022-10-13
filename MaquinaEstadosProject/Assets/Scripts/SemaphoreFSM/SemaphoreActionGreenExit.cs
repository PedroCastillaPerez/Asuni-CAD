using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionGreenExit")]
public class SemaphoreActionGreenExit : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.greenLightC.material = s.offMaterial;

        Debug.Log("Green exit");


    }
}
