using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionCube : MonoBehaviour
{
    public Transform semaphore;

    public enum Action
    {
        sendEvent,
        exit
    };

    public Action action;

    public Semaphore.Event eventToSend;
    bool send;

    bool exit;

    Semaphore semaphoreC;

    const float rotationSpeed = 270;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        semaphoreC = semaphore.GetComponent<Semaphore>();
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        if(angle >= 360) { angle -= 360; }

        if(send)
        {
            semaphoreC.OnEventReceived(eventToSend);
        }
        else if(exit)
        {
            Application.Quit(0);
        }

        transform.rotation = Quaternion.Euler(0, angle, 0);

        send = false;
        exit = false;
    }

    void OnMouseDown()
    {
        if(action == Action.sendEvent) { send = true; }
        else { exit = true; }
        
        Update();
    }

}
