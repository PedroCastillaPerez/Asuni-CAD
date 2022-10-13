using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/DecisionAnyToRed")]
public class SemaphoreDecisionAnyToRed : Decision
{
    public override bool Decide(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        if(s.hasReceivedEvent && s.receivedEvent == Semaphore.Event.gotoRed)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
