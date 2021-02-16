using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigidbody : MonoBehaviour
{
    float forwardSpeed = 8f;
    float sideToSideSpeed = 7f;
    float backSpeed = 6f;
    private float targetSpeed = 0f;
    public bool canDoInput = true;


    public bool isGrounded = true;
    [SerializeField] Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask ground;
    public Rigidbody rbody;
    private float jumpForce = 1000f;
    public float dashForce = 100f;

    [Header("Parkour")]
    public bool isWallRunning;
    public bool isWallRunningRight;
    public bool isWallRunningLeft;
    private float wallRunUpForce = 10f;
    private float currentWallRunUpForce = 0f;
    private float wallRunDecreaseRate = 25f;
    private float jumpOffWallUpForce = 2f;
    private float jumpOffWallForwardForce = 40f;
    public bool justJumpedOffWall = false;
    private WallJumpBox wallJumpBox;
    public string lastWallRunDirection;

    private float dashDurationSeconds = 1f;
    void Awake()
    {
        wallJumpBox = GetComponentInChildren<WallJumpBox>();
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float resultSpeedBasedOnDirection = ProcessMovment(x,z);
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        if (canDoInput)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }

            if (isGrounded && !isWallRunning)
            {
                rbody.velocity = new Vector3(move.x * Time.deltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.deltaTime * 100f * resultSpeedBasedOnDirection);
            }

            if (!isGrounded)
            {
                rbody.velocity = new Vector3(move.x * Time.deltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.deltaTime * 100f * resultSpeedBasedOnDirection);
            }

            if (!isWallRunning)
            {
                currentWallRunUpForce = wallRunUpForce;
            }
            if (isWallRunning)
            {
                rbody.velocity = new Vector3(rbody.velocity.x * 2, currentWallRunUpForce, rbody.velocity.z * 2);
                currentWallRunUpForce -= wallRunDecreaseRate * Time.deltaTime;
                if (isWallRunningRight) lastWallRunDirection = "right";
                else lastWallRunDirection = "left";
            }

            if(wallJumpBox.canWallJump && !isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if (lastWallRunDirection == "right")
                        rbody.velocity = -transform.right * jumpOffWallForwardForce + transform.forward * jumpOffWallForwardForce + transform.up * jumpOffWallUpForce; //+ transform.up * jumpOffWallUpForce;
                    else if(lastWallRunDirection == "left")
                        rbody.velocity = transform.right * jumpOffWallForwardForce + transform.forward * jumpOffWallForwardForce + transform.up * jumpOffWallUpForce; //+ transform.up * jumpOffWallUpForce;
                    StartCoroutine(ChangeCanDoInput());
                    justJumpedOffWall = true;
                    ResetWallRun();
                }
            }
        }

        if(isGrounded)
        {
            justJumpedOffWall = false;
            ResetWallRun();
        }
    }

    public void ResetWallRun()
    {
        isWallRunning = false;
        isWallRunningLeft = false;
        isWallRunningRight = false;
    }

    public float ProcessMovment(float x = 0f, float z = 0f)
    {
        if (x > 0.1f || x < -0.1f)
        {
            targetSpeed = sideToSideSpeed;
        }

        if (z < -0.1f)
        {
            targetSpeed = backSpeed;
        }

        if (z > 0.1f)
        {
            targetSpeed = forwardSpeed;
        }

        targetSpeed = isWallRunning ? targetSpeed * 1.2f : justJumpedOffWall ? targetSpeed * 1.5f : !isGrounded ? targetSpeed * 0.6f : targetSpeed;
        return targetSpeed;
    }
    IEnumerator ChangeCanDoInput()
    {
        canDoInput = false;
        yield return new WaitForSeconds(0.25f);
        canDoInput = true;
        justJumpedOffWall = false;
    }
}


