using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/DecisionAnyToAmber")]
public class SemaphoreDecisionAnyToAmber : Decision
{
    public override bool Decide(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        if(s.hasReceivedEvent && s.receivedEvent == Semaphore.Event.gotoAmber)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
