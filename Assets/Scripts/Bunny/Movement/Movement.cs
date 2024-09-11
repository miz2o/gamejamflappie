using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Movement : MonoBehaviour
{
    private BunnyInput bunnyInput;
    private InputAction move, rotate;

    public Rigidbody rb;
    public Camera camHolder;

  
    public float jumpForce, sensitivity, moveSpeed, sprintspeed, currentSpeed;
    float maxJumpForce;

    public Vector3 forceDirection;
    public Vector3 jump;

    private Vector2 look;
    private float x, y;


    public bool isGrounded, jumping;

    RaycastHit hit;

    private void Awake()
    {
        bunnyInput = new BunnyInput();

        jump = new Vector3 (0, 10, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }

    private void OnEnable()
    {
        move = bunnyInput.Bunny.Move;
        move.Enable();
        rotate = bunnyInput.Bunny.Mouse;
        bunnyInput.Bunny.Mouse.Enable();
        rotate.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
    }


    private void Update()
    {
        Rotate();
    }
    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(camHolder) * currentSpeed;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(camHolder) * currentSpeed;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * 3.5f * Time.deltaTime;
        }


        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.2f))
        {
            if (hit.transform.tag != "Jumpable")
            {
                return;
            }
            if (!isGrounded)
            {
                isGrounded = true;
                jumping = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        //animations
        /*float actualSpeed = new Vector3(rb.velocity.x, rb.velocity.z, 0).magnitude;
        movementAnimator.SetFloat("Actual Speed", actualSpeed);*/
    }

    private Vector3 GetCameraForward(Camera cam)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }
    private Vector3 GetCameraRight(Camera cam)
    {
        Vector3 right = cam.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    private void Rotate()
    {
        look = rotate.ReadValue<Vector2>();

        x += look.x * sensitivity;
        y -= look.y * sensitivity;

        y = Mathf.Clamp(y, -85, 85);

        transform.localRotation = Quaternion.Euler(0, x, 0 * Time.deltaTime);
        camHolder.transform.localRotation = Quaternion.Euler(y, 0, 0 * Time.deltaTime);
    }
    public void DoJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (isGrounded)
            {
                Debug.Log("JUMP");
                 //movementAnimator.SetTrigger("Jumping");
                jumping = true;
                isGrounded = false;
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            }

        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            currentSpeed = moveSpeed + sprintspeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }


}
