using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController  MonoBehaviour
{
    public Transform playerArms;

    public float gravity = 6;

    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private float walkSpeed = 4f;
    [SerializeField]
    private float sprintSpeed = 6f;
    [SerializeField]
    private float jumpSpeed = 30f;
    
    

    private CharacterController controller;
    private Vector3 moveDirection;

    private float moveSpeed;
    private float xAxisClamp;
    private void Start()
    {
        controller = GetComponentCharacterController();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RotateCamera();
        Move();
    }

    
      Player Rotate Function.
     
    void RotateCamera()
    {
        float mouseX = Input.GetAxis(Mouse X);
        float mouseY = Input.GetAxis(Mouse Y);

        float rotAmountX = mouseX  mouseSensitivity;
        float rotAmountY = mouseY  mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 rotPlayerArms = playerArms.transform.rotation.eulerAngles;
        Vector3 rotPlayer = transform.rotation.eulerAngles;

        rotPlayerArms.x -= rotAmountY;
        rotPlayerArms.z = 0;
        rotPlayer.y += rotAmountX;

        if (xAxisClamp  90)
        {
            xAxisClamp = 90;
            rotPlayerArms.x = 90;
        }
        else if (xAxisClamp  -90)
        {
            xAxisClamp = -90;
            rotPlayerArms.x = 270;
        }

        playerArms.rotation = Quaternion.Euler(rotPlayerArms);
        transform.rotation = Quaternion.Euler(rotPlayer);
    }

    
      Player Move Function.
     
    void Move()
    {
        float moveX = Input.GetAxis(Horizontal);
        float moveZ = Input.GetAxis(Vertical);

        if (controller.isGrounded) {
            moveDirection = new Vector3(moveX, 0, moveZ);
            moveDirection = transform.TransformDirection(moveDirection);
            if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
                moveSpeed = sprintSpeed;
            else
                moveSpeed = walkSpeed;

            moveDirection = moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y += jumpSpeed;
            }
        }
        moveDirection.y -= gravity  Time.deltaTime;

        controller.Move(moveDirection  Time.deltaTime);
    }
}