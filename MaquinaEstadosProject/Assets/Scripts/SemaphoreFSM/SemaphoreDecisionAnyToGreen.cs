using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/DecisionAnyToGreen")]
public class SemaphoreDecisionAnyToGreen : Decision
{
    public override bool Decide(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        if(s.hasReceivedEvent && s.receivedEvent == Semaphore.Event.gotoGreen)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
