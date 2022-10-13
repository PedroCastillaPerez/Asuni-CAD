using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Semaphore/ActionRedUpdate")]
public class SemaphoreActionRedUpdate : Action
{
    public override void Act(Controller controller)
    {
        Semaphore s = (Semaphore)controller;

        s.timerTextC.text = "Temporizador: " + System.String.Format("{0:00.00}", s.changeTimer);

        s.changeTimer -= Time.deltaTime;
    }
}
