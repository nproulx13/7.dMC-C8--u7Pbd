using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigidbody : MonoBehaviour
{
    private bool isGrounded = true;
    [SerializeField] Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask ground;
    public Rigidbody rbody;
    public float jumpForce = 20f;
    public float speed = 750f;
    public float dashForce = 100f;

    private float dashDurationSeconds = 1f;
    void Start()
    {
        
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //get direction of dash at time of dash
        //add that to the velocity for 1 second)
        rbody.velocity = new Vector3(move.x * speed * Time.deltaTime, rbody.velocity.y, move.z * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            rbody.AddForce(-transform.right * dashForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rbody.AddForce(transform.right * dashForce, ForceMode.Impulse);
        }
    }
}
