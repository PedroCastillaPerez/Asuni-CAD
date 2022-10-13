using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Semaphore : Controller
{
    public enum Event
    {
        gotoRed,
        gotoAmber,
        gotoGreen,
        reset

    };

    [Header("Semaphore")]

    public Transform stateText;
    public Transform timerText;

    public Transform redLight;
    public Transform amberLight;
    public Transform greenLight;

    public Material offMaterial;
    public Material redMaterial;
    public Material amberMaterial;
    public Material greenMaterial;

    public float redDuration = 10;
    public float amberDuration = 3;
    public float greenDuration = 10;


    [Header("Only for state machine communication. Don't edit")]

    public TextMesh stateTextC;
    public TextMesh timerTextC;

    public MeshRenderer redLightC;
    public MeshRenderer amberLightC;
    public MeshRenderer greenLightC;

    public bool hasReceivedEvent = true;
    public Event receivedEvent;

    public float changeTimer;


    // Start is called before the first frame update
    override public void Start()
    {
        stateTextC = stateText.GetComponent<TextMesh>();
        timerTextC = timerText.GetComponent<TextMesh>();

        redLightC = redLight.GetComponent<MeshRenderer>();
        amberLightC = amberLight.GetComponent<MeshRenderer>();
        greenLightC = greenLight.GetComponent<MeshRenderer>();

        base.Start();
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();

        hasReceivedEvent = false;
        
    }


    public void OnEventReceived(Semaphore.Event e)
    {
        hasReceivedEvent = true;
        receivedEvent = e;

        Update();
    }

}
