using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public bool adoptPerspective;
    public bool adoptSize;
    public bool adoptFov;


    bool started;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        started = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Camera GetCamera()
    {
        if(!started) { Start(); started = true; }

        return camera;
    }
}
