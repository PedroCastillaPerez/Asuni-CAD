using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DemoManager : MonoBehaviour
{
    public Transform followCamera;
    public Transform demoTitle;
    public Transform demoDescription;

    // Linear interpolation controls

    public Transform incrementFactor;
    public Transform decrementFactor;


    float factor;


    [System.Serializable]
    public struct DemoSettings
    {
        public Transform cameraTarget;
        public string title;
        [TextArea]
        public string description;
        public bool teleportCamera;


    }

    public DemoSettings[] demoSettings;

    public int initialDemoIndex = 0;

    FollowCamera followCameraC;
    CameraTarget cameraTargetC;
    TextMesh demoTitleC;
    TextMesh demoDescriptionC;

    int demoIndex;


    // Start is called before the first frame update
    void Start()
    {
        followCameraC = followCamera.GetComponent<FollowCamera>();
        demoTitleC = demoTitle.GetComponent<TextMesh>();
        demoDescriptionC = demoDescription.GetComponent<TextMesh>();



        demoIndex = initialDemoIndex;
        DoSetupDemo();

    }

    // Update is called once per frame
    void Update()
    {
        DoUpdateDemo();
    }

    public void OnNextDemoPressed()
    {
        demoIndex = ( demoIndex + 1 ) % demoSettings.Length;
        DoSetupDemo();

    }

    public void OnPreviousDemoPressed()
    {
        demoIndex = (demoIndex - 1 < 0 ? demoSettings.Length - 1 : demoIndex - 1) % demoSettings.Length;
        DoSetupDemo();

    }

    public void OnIncrementFactorPressed()
    {
        factor += 0.2f * Time.deltaTime;
        if(factor > 1) { factor = 1; }
    }

    public void OnDecrementFactorPressed()
    {
        factor -= 0.2f * Time.deltaTime;
        if(factor < 0) { factor = 0; }
    }

    public void OnQuitPressed()
    {
        Application.Quit(0);
    }




    void DoSetupDemo()
    {
        DemoSettings d = demoSettings[demoIndex];

        cameraTargetC = d.cameraTarget.GetComponent<CameraTarget>();

        if (d.teleportCamera)
        {
            followCameraC.transform.position = d.cameraTarget.transform.position;
            followCameraC.transform.rotation = d.cameraTarget.transform.rotation;
        }


        followCameraC.target = d.cameraTarget;

        if(cameraTargetC.adoptPerspective)
        {
            followCameraC.GetCamera().orthographic = cameraTargetC.GetCamera().orthographic;
        }

        if (cameraTargetC.adoptSize)
        {
            followCameraC.GetCamera().orthographicSize = cameraTargetC.GetCamera().orthographicSize;
        }

        if (cameraTargetC.adoptFov)
        {
            followCameraC.GetCamera().fieldOfView = cameraTargetC.GetCamera().fieldOfView;
        }


        demoTitleC.text = d.title;
        demoDescriptionC.text = d.description;



 

    }

    void DoUpdateDemo()
    {

    }


}
