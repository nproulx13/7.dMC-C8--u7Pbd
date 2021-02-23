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
    private float jumpForce = 1200f;
    private float slideForce = 75f;
    private float dashForce = 37.5f;
    private bool canDash = true;
    Vector3 move;

    [Header("Parkour")]
    public GameObject lastWall;
    public Vector3 lastNormalVector1;
    public Vector3 lastNormalVector2;
    public int oneOrTwoSwitchForNormalVectors = 1;
    public bool isWallRunning;
    public bool isWallRunningRight;
    public bool isWallRunningLeft;
    private float wallRunUpForce = 8.5f;
    private float currentWallRunUpForce = 0f;
    private float wallRunDecreaseRate = 25f;
    private float jumpOffWallUpForce = 7.5f;
    private float jumpOffWallForwardForce = 18.5f;
    public bool justJumpedOffWall = false;
    private WallJumpBox wallJumpBox;
    public string lastWallRunDirection;
    [SerializeField] private Animator headCamera;
    [SerializeField] private CapsuleCollider capsuleCollider; 

    private float dashDurationSeconds = 1f;
    void Awake()
    {
        wallJumpBox = GetComponentInChildren<WallJumpBox>();
    }

    
    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);
        if(isWallRunningRight)
        {
            headCamera.SetBool("Right", true);
        }
        else if (isWallRunningLeft)
        {
            headCamera.SetBool("Left", true);
        }
        else
        {
            headCamera.SetBool("Right", false);
            headCamera.SetBool("Left", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        if (wallJumpBox.canWallJump && !isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (lastWallRunDirection == "right")
                    rbody.velocity = -transform.right * 0.75f * jumpOffWallForwardForce + transform.forward * jumpOffWallForwardForce * 1.25f + transform.up * jumpOffWallUpForce * 3f; //+ transform.up * jumpOffWallUpForce;
                else if (lastWallRunDirection == "left")
                    rbody.velocity = transform.right * jumpOffWallForwardForce + transform.forward * jumpOffWallForwardForce * 1.25f + transform.up * jumpOffWallUpForce * 3f; //+ transform.up * jumpOffWallUpForce;
                StartCoroutine(ChangeCanDoInput());
                justJumpedOffWall = true;
                ResetWallRun();
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            capsuleCollider.height = 0.5f;
            capsuleCollider.center = new Vector3(0, 0.25f, 0);
            rbody.velocity = (transform.forward.normalized * slideForce / 2) + -transform.up * 5f;
            StartCoroutine(Sliding());
        }

        if(Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            rbody.velocity = -transform.right.normalized * dashForce + transform.forward.normalized * 2 + move;
            StartCoroutine(Dashing());
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            rbody.velocity = transform.right.normalized * dashForce + transform.forward.normalized * 2 + move;
            StartCoroutine(Dashing());
        }

        if (isGrounded)
        {
            lastWall = null;
            justJumpedOffWall = false;
            ResetWallRun();
        }

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float resultSpeedBasedOnDirection = ProcessMovment(x,z);
        move = (transform.right * x + transform.forward * z).normalized;

        if ((z != 0 || x != 0) && isGrounded) 
            headCamera.SetBool("Running", true);
        else 
            headCamera.SetBool("Running", false);

        if (canDoInput && canDash)
        {
            if (isGrounded && !isWallRunning)
            {
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            if (!isGrounded)
            {
                rbody.velocity = new Vector3(move.x * Time.deltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            if (!isWallRunning)
            {
                currentWallRunUpForce = wallRunUpForce;
            }
            if (isWallRunning)
            {
                if (isWallRunningRight)
                {
                    rbody.velocity = new Vector3(rbody.velocity.x * 1.25f, currentWallRunUpForce, rbody.velocity.z * 1.25f) + transform.right.normalized * 2;
                    lastWallRunDirection = "right";
                }
                else
                {
                    rbody.velocity = new Vector3(rbody.velocity.x * 1.25f, currentWallRunUpForce, rbody.velocity.z * 1.25f) + -transform.right.normalized * 2;
                    lastWallRunDirection = "left";
                }
                currentWallRunUpForce -= wallRunDecreaseRate * Time.fixedDeltaTime;
            }
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

        targetSpeed = isWallRunning ? targetSpeed * 1.3f : justJumpedOffWall ? targetSpeed * 2f : !isGrounded ? targetSpeed * 0.9f : targetSpeed;
        return targetSpeed;
    }
    IEnumerator ChangeCanDoInput()
    {
        canDoInput = false;
        yield return new WaitForSeconds(0.25f);
        canDoInput = true;
        justJumpedOffWall = false;
    }

    IEnumerator Sliding()
    {
        canDoInput = false;
        yield return new WaitForSeconds(0.5f);
        canDoInput = true;
        capsuleCollider.height = 2f;
        capsuleCollider.center = new Vector3(0, 0, 0);
    }

    IEnumerator Dashing()
    {
        canDash = false;
        yield return new WaitForSeconds(0.2f);
        canDash = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("WallRun"))
        {
            ContactPoint[] contacts = collision.contacts;
            Vector3 normal = contacts[0].normal;
            Debug.DrawRay(contacts[0].point, normal, Color.cyan, 5f);
            if (oneOrTwoSwitchForNormalVectors == 1)
            {
                lastNormalVector1 = normal;
                oneOrTwoSwitchForNormalVectors = 2;
            }
            else
            {
                lastNormalVector2 = normal;
                oneOrTwoSwitchForNormalVectors = 1;
            }
        }
    }
}


