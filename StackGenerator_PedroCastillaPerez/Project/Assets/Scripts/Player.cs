using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform model;
    public Transform pivot;

    public float walkSpeed = 10;

    public float jumpSpeed = 10;

    public GameObject barrel;

    public Vector3 barrelSpawnOffset;

    public float groundCheckDistance = 0.1f;

    public Transform cameraTarget;

    float inputX;
    float inputZ;
    float inputAmount;

    bool jump;

    bool isGrounded;
    
    Animator animatorC;
    Rigidbody rigidC;

    Quaternion inputLook;

    bool spawnWithDontDestroy;

    CapsuleCollider colliderC;


    // Start is called before the first frame update
    void Start()
    {
        animatorC = model.GetComponent<Animator>();

        rigidC = GetComponent<Rigidbody>();
        colliderC = GetComponent<CapsuleCollider>();


        jump = false;

        spawnWithDontDestroy = true;

        isGrounded = true;

    }

    // Update is called once per frame
    void Update()
    {
        bool applyLook = false;
        bool spawnBarrel = false;

        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        inputAmount = (new Vector3(inputX, 0, inputZ)).magnitude;




        if (!Mathf.Approximately(inputX, 0) || !Mathf.Approximately(inputZ, 0))
        {
            Vector3 inputDirection = cameraTarget.TransformVector(new Vector3(inputX, 0, inputZ));
            inputDirection = new Vector3(inputDirection.x, 0, inputDirection.z);
            inputDirection = inputDirection.normalized;

            inputLook = Quaternion.LookRotation(inputDirection, Vector3.up) ; applyLook = true;
        }
    

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) { jump = true; }
        if(Input.GetKeyDown(KeyCode.Return)) { spawnBarrel = true; }

        if(applyLook)
        {
            pivot.localRotation = inputLook;


        }

        if(spawnBarrel)
        {
            GameObject go = (GameObject)GameObject.Instantiate(barrel, pivot.TransformPoint(barrelSpawnOffset), Quaternion.identity);

            if(spawnWithDontDestroy) { GameObject.DontDestroyOnLoad(go); }
        }


    }

    void FixedUpdate()
    {
        Vector3 point1L = colliderC.center - Vector3.up * (colliderC.height - 2 * colliderC.radius) / 2;
        Vector3 point2L = colliderC.center + Vector3.up * (colliderC.height - 2 * colliderC.radius) / 2;
        Vector3 point1W = transform.TransformPoint(point1L) + Vector3.up * 0.1f;
        Vector3 point2W = point1W + Vector3.up * (colliderC.height - 2 * colliderC.radius);


        if (Physics.CapsuleCast(point1W, point2W, colliderC.radius, -Vector3.up, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



        Vector3 speed = rigidC.velocity;

        speed = new Vector3(0, speed.y, 0);

        if(jump)
        {
            speed += Vector3.up * jumpSpeed;
        }

        float forwardSpeed;

        forwardSpeed = walkSpeed * inputAmount;

        speed += pivot.TransformVector(Vector3.forward * forwardSpeed);

        //Debug.Log("Speed " + speed);

        animatorC.SetFloat("ForwardSpeed", forwardSpeed / walkSpeed);
        rigidC.velocity = speed;

        jump = false;

    }

    public void SetSpawnWithDontDestroyEnabled(bool value)
    {
        spawnWithDontDestroy = value;
    }
}
