using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    private float runSpeed = 10f;
    private float airSpeed = 5f;
    private float wallRunSpeed = 10f;
    private float jumpForce = 2f;

    [Header("Character Controller")]
    public CharacterController controller;

    [Header("Grounded Checks")]
    public Transform groundCheck;
    public LayerMask ground;
    [SerializeField] private bool isGrounded = true;

    private Vector3 velocity;
    private float gravity = -9.81f * 1f;
    private float groundDistance = 0.4f;

    [Header("Parkour")]
    public bool isWallRunning = false;
    public bool isWallRunningLeft = false;
    public bool isWallRunningRight = false;
    private float noWallRunAngle = 0f;
    private float rightWallRunAngle = 22.5f;
    private float leftWallRunAngle = -22.5f;
    private float timeStartedWallRunning = 0f;
    private float angleChangeTime = 1f;
    [SerializeField] private WallRun lastWallRun;
    [SerializeField] private bool startedResetTilt;
    [SerializeField] private bool doLerp = false;
    private float heightOfContact = 0f;
    public Vector3 wallRunDirection;
    [SerializeField] private GameObject WallRunBoxLeft;
    [SerializeField] private GameObject WallRunBoxRight;


    private enum WallRun
    {
        None, Left, Right
    };

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Clamp(transform.rotation.eulerAngles.z, -25f, 25f));
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground); 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //-2 constant down velocity because 0 makes it bounce when going down ramps 
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //the local right times the x input, the local forward times the z input

        Vector3 move;
        //if (isWallRunning)
        //{
        //    move = wallRunDirection;
        //}
        //else
            move = transform.right * x + transform.forward * z;

        //if (isGrounded) 
            Move(move, runSpeed); //grounded
        //else if (isWallRunning) 
            //Move(move, wallRunSpeed); //wall running
        //else 
           // Move(move, airSpeed); //in air

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        //if(isWallRunning)
            //transform.position = new Vector3(transform.position.x, Mathf.Clamp(heightOfContact+=(velocity.y*Time.deltaTime), -10000, heightOfContact + 0.5f), transform.position.z);
       // else
            controller.Move(velocity * Time.deltaTime);

        ///////////////////////////////////Parkour//////////////////////////////////////////
        
        if(isWallRunningRight)
        {
            StartCoroutine(ChangeTilt(true));
        }

    }

    private void Move(Vector3 move, float speed)
    {
        controller.Move(move * speed *Time.deltaTime);
    }

    private float DoTiltLerp(float timeStarted, float startAngle, float endAngle)
    {
        float timeSinceStartedWallRunning = Time.time - timeStarted;
        float percentOfLerpDone = timeSinceStartedWallRunning / angleChangeTime;
        transform.rotation = Quaternion.Euler(Vector3.Lerp(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, startAngle), 
            new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, endAngle), percentOfLerpDone));
        return percentOfLerpDone;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawRay(hit.point, hit.normal, Color.green, 5f);
        Debug.DrawRay(hit.point, -Vector3.Cross(hit.normal.normalized, Vector3.up), Color.white, 5f);
        //wallRunDirection = -Vector3.Cross(hit.normal.normalized, Vector3.up);
    }

    public IEnumerator ChangeTilt(bool isRightSide)
    {
        if(isRightSide)
        {
            float timeStartedTilt = Time.time;
            float startAngle = transform.rotation.eulerAngles.z;
            float percentOfLerpDone = 0;
            print(percentOfLerpDone);
            while (percentOfLerpDone < 1)
            {
                percentOfLerpDone = DoTiltLerp(timeStartedTilt, startAngle, rightWallRunAngle);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {

        }
        yield return null;
    }
}
