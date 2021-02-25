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
    private bool dashing = false;
    Vector3 move;

    [Header("Parkour")]
    public GameObject lastWall1;
    public GameObject lastWall2;
    public int oneOrTwoSwitchForWalls= 1;
    //public Vector3 lastNormalVector1;
    //public Vector3 lastNormalVector2;
    public Vector3 lastNormalVector;
    public int oneOrTwoSwitchForNormalVectors = 1;
    public bool isWallRunning;
    public bool isWallRunningRight;
    public bool isWallRunningLeft;
    private float wallRunUpForce = 12f;
    private float currentWallRunUpForce = 0f;
    private float wallRunDecreaseRate = 25f;
    private float jumpOffWallUpForce = 30.5f;
    private float jumpOffWallForwardForce = 23.5f;
    public bool justJumpedOffWall = false;
    [SerializeField] private Animator headCamera;
    [SerializeField] private CapsuleCollider capsuleCollider; 
    public Vector3 wallRunVelocity;
    public WallRunBaseBox wallRunBaseBox;
    public GameObject wallRunRig;
    public Quaternion rigRotation;
    public bool getNextWall = true;
    private Vector3 jumpedOfWallVelocity = Vector3.zero;

    private float dashDurationSeconds = 1f;
    void Awake()
    {
        getNextWall = true;
    }

    
    void Update()
    {
        if(isWallRunningRight)
        {
            justJumpedOffWall = false;
            canDoInput = true;
            headCamera.SetBool("Right", true);
        }
        else if (isWallRunningLeft)
        {
            justJumpedOffWall = false;
            canDoInput = true;
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

        if (isWallRunning && !isGrounded && !justJumpedOffWall)
        {
            if (Input.GetButtonDown("Jump"))
            {
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");

                if (Mathf.Abs(x) > 0 && (Mathf.Abs(x) > Mathf.Abs(z/2)))
                {
                    Vector3 jumpOffWallSideForce = isWallRunningRight ? -transform.right : transform.right;
                    rbody.velocity = jumpOffWallForwardForce / 50 * transform.forward + transform.up * jumpOffWallUpForce * 1.55f + jumpOffWallSideForce * 10f;
                    jumpedOfWallVelocity = rbody.velocity / 2;
                }
                else 
                {
                    Vector3 jumpOffWallSideForce = isWallRunningRight ? -transform.right : transform.right;
                    rbody.velocity = jumpOffWallForwardForce * transform.forward + transform.up * jumpOffWallUpForce + jumpOffWallSideForce * 2.5f;
                    jumpedOfWallVelocity = rbody.velocity / 2;
                }

                justJumpedOffWall = true;
                getNextWall = true;

                isWallRunning = false;
                isWallRunningLeft = false;
                isWallRunningRight = false;
                canDash = true;
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift) && !isWallRunning)
        {
            capsuleCollider.height = 0.5f;
            capsuleCollider.center = new Vector3(0, 0.25f, 0);
            rbody.velocity = (transform.forward.normalized * slideForce / 2) + -transform.up * 5f;
            StartCoroutine(Sliding());
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isWallRunning && !isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y - 60f * Time.deltaTime, rbody.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Q) && canDash && !isWallRunning)
        {
            rbody.velocity = -transform.right.normalized * dashForce + transform.forward.normalized * 1.5f + move;
            StartCoroutine(Dashing());
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash && !isWallRunning)
        {
            rbody.velocity = transform.right.normalized * dashForce + transform.forward.normalized * 1.5f + move;
            StartCoroutine(Dashing());
        }

        if (isGrounded)
        {
            canDoInput = true;
            lastNormalVector = Vector3.zero;
            //lastNormalVector1 = Vector3.zero;
            //lastNormalVector2 = Vector3.zero;
            getNextWall = true;
            ResetWallRun();
        }

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float resultSpeedBasedOnDirection = ProcessMovment(x, z);
        move = (transform.right * x + transform.forward * z).normalized;

        if ((z != 0 || x != 0) && isGrounded)
            headCamera.SetBool("Running", true);
        else
            headCamera.SetBool("Running", false);

        if (justJumpedOffWall && !dashing)
        {
            //move = (transform.right * x + transform.forward * z).normalized;
            Vector3 jumpedOffWallAirSpeed = new Vector3(move.x * Time.deltaTime * 100f * resultSpeedBasedOnDirection, 0, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            rbody.velocity = new Vector3(jumpedOfWallVelocity.x, rbody.velocity.y - 20f * Time.deltaTime, jumpedOfWallVelocity.z) + jumpedOffWallAirSpeed;
        }

        else if (canDoInput && !dashing)
        {
            if (isGrounded && !isWallRunning)
            {
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            else if (!isGrounded && !isWallRunning)//air speed
            {
                rbody.velocity = new Vector3(move.x * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection, rbody.velocity.y, move.z * Time.fixedDeltaTime * 100f * resultSpeedBasedOnDirection);
            }

            else if (isWallRunning)
            {
                wallRunRig.transform.rotation = rigRotation;
                if (isWallRunningRight && z > float.Epsilon)
                {
                    rbody.velocity = new Vector3(wallRunVelocity.x * 2f, currentWallRunUpForce, wallRunVelocity.z * 2f) + transform.right.normalized * 2;
                }
                else if(isWallRunningLeft && Mathf.Abs(z) > float.Epsilon)
                {
                    rbody.velocity = new Vector3(wallRunVelocity.x * 2f, currentWallRunUpForce, wallRunVelocity.z * 2f) + -transform.right.normalized * 2;
                }
                currentWallRunUpForce -= wallRunDecreaseRate * Time.fixedDeltaTime;
            }

            if (!isWallRunning)
            {
                getNextWall = true;
                wallRunRig.transform.localRotation = Quaternion.Euler(0,0,0);
                currentWallRunUpForce = wallRunUpForce;
            }
        }
    }

    public void ResetWallRun()
    {
        canDoInput = true;
        isWallRunning = false;
        isWallRunningLeft = false;
        isWallRunningRight = false;
        lastWall1 = null;
        lastWall2 = null;
        justJumpedOffWall = false;
        getNextWall = true;
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

        targetSpeed = isWallRunning ? targetSpeed * 1.7f : justJumpedOffWall ? targetSpeed * 1.2f : !isGrounded ? targetSpeed * 0.9f : targetSpeed;
        return targetSpeed;
    }
    IEnumerator ChangeCanDoInput()
    {
        canDoInput = false;
        yield return new WaitForSeconds(0.2f);
        canDoInput = true;
    }

    IEnumerator Sliding()
    {
        headCamera.SetTrigger("Slide");
        canDoInput = false;
        yield return new WaitForSeconds(1f);
        canDoInput = true;
        capsuleCollider.height = 2f;
        capsuleCollider.center = new Vector3(0, 0, 0);
    }

    IEnumerator Dashing()
    {
        dashing = true;
        canDash = false;
        yield return new WaitForSeconds(0.3f);

        dashing = false;

        yield return new WaitForSeconds(0.7f);
        canDash = true;
    }

    public Vector3 GetVelocity()
    {
        return transform.forward.normalized * 15f;
    }

    public void SetLastWalls(GameObject wall)
    {
        if(oneOrTwoSwitchForWalls == 1)
        {
            lastWall1 = wall;
            oneOrTwoSwitchForWalls = 2;
        }
        else if(oneOrTwoSwitchForWalls == 2)
        {
            lastWall2 = wall;
            oneOrTwoSwitchForWalls = 1;
        }
    }

    /*
    public void SetNormals(Vector3 angles)
    {
        if (oneOrTwoSwitchForNormalVectors == 1)
        {
            lastNormalVector1  = angles;
            oneOrTwoSwitchForNormalVectors = 2;
        }
        else if (oneOrTwoSwitchForNormalVectors == 2)
        {
            lastNormalVector2 = angles;
            oneOrTwoSwitchForNormalVectors = 1;
        }
    }*/
}


