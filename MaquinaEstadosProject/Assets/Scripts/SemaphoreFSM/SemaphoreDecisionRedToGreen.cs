using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/DecisionRedToGreen")]
public class SemaphoreDecisionRedToGreen : Decision
{
    public override bool Decide(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        if (s.changeTimer <= 0) { return true; }
        else { return false; }

    }
}
