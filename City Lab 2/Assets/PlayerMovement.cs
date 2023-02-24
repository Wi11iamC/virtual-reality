using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    // Start is called before the first frame update
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        //handle drag

        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        movePlayer();
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
         
    }

    private void movePlayer(){
        //calc movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded) {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed *  10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl() {
    Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    // max speed
    if (flatvel.magnitude > moveSpeed) { 
        Vector3 limitedVel = flatvel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVel.x, 0f, limitedVel.z);
    }
}

private void Jump(){
    // reset y.velocity

    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

}

private void ResetJump(){
    readyToJump = true;
}
}
