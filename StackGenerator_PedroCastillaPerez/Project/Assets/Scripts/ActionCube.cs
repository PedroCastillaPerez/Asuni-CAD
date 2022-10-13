using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionCube : MonoBehaviour
{

    public enum Action
    {
        nextDemo,
        exit,
        incrementFactor,
        decrementFactor,
        previousDemo
    };

    public Action action;


    const float rotationSpeed = 270;
    float angle;

    bool tryAction;


    DemoManager manager;


    // Start is called before the first frame update
    void Start()
    {
        angle = 0;

        manager = GameObject.FindWithTag("DemoManager").GetComponent<DemoManager>();



    }

    // Update is called once per frame
    void Update()
    {
        if(tryAction)
        {

            angle += rotationSpeed * Time.deltaTime;
            if(angle >= 360) { angle -= 360; }

            // Continous actions

            if(Input.GetMouseButton(0))
            {
                if (action == Action.incrementFactor)
                {
                    manager.OnIncrementFactorPressed();
                }
                else if (action == Action.decrementFactor)
                {
                    manager.OnDecrementFactorPressed();
                }
            }

            // Discrete actions

            if(Input.GetMouseButtonDown(0))
            {
                if (action == Action.exit)
                {
                    manager.OnQuitPressed();
                }
                else if(action == Action.nextDemo)
                {
                    manager.OnNextDemoPressed();
                }
                else if(action == Action.previousDemo)
                {
                    manager.OnPreviousDemoPressed();
                }
            }



            transform.rotation = Quaternion.Euler(0, angle, 0);

        }

        tryAction = false;
    }

    void OnMouseOver()
    {
        tryAction = true;

    }

}
