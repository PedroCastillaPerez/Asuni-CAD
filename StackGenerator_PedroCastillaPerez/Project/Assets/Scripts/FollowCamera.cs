using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    public bool lockHeight;

    public float speed = 10;

    public Vector3 aimOffset;

    Camera cameraC;

    bool started;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 displacement = target.position - transform.position;
        Vector3 projectedDisplacement = new Vector3(displacement.x, 0, displacement.z);
        transform.position += projectedDisplacement; 
        transform.rotation = target.rotation;

        cameraC = GetComponent<Camera>();

        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = target.TransformPoint(aimOffset) - transform.position;
        Vector3 projectedDisplacement = new Vector3(displacement.x, lockHeight ? 0 : displacement.y, displacement.z);

        transform.position += projectedDisplacement.normalized * Time.deltaTime * speed * projectedDisplacement.magnitude;
        transform.rotation = target.rotation;
    }

    public Camera GetCamera()
    {
        if (!started) { Start(); started = true; }

        return cameraC;
    }
}
