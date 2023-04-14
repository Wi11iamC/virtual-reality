using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPC_script_namespace{
[RequireComponent(typeof(CharacterController))]
public class FPC_script : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    // 0 for red (fire) 1 for (ice) 
    private KeyCode[] keyCodes = new KeyCode []{ KeyCode.Alpha0, KeyCode.Alpha1};
    public float wand_selected = 1;


    public bool is_shooting = false;

    [SerializeField]
    public GameObject primary_wand;
    [SerializeField]
    public GameObject secondary_wand;
    [SerializeField]
    
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
    }

    // Update is called once per frame
    void Update()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Wand Selection
        if (Input.GetKeyDown(keyCodes[0]))
        {
            wand_selected = 0;
        } 
        if (Input.GetKeyDown(keyCodes[1])){
            wand_selected = 1;
        }


        if (wand_selected == 1) {
            primary_wand.SetActive(false); // false to hide
            secondary_wand.SetActive(true); // false to hide
        } else {
            wand_selected = 0;
            secondary_wand.SetActive(false); // false to hide
            primary_wand.SetActive(true); // false to hide
        }
        #endregion

        #region Handles Shooting
        if (Input.GetMouseButtonDown(0))
        {
            is_shooting=true;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }
}
}