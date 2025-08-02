using UnityEngine;
using UnityEngine.InputSystem;

// クラス名だけ Player2Controller に変更
public class Player2Controller : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float dashSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;
    private bool isGrounded;
    private Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isDash;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        float currentSpeed = isDash ? dashSpeed : walkSpeed;
        
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 finalMoveDirection = cameraForward * moveInput.y + cameraTransform.right * moveInput.x;

        if (finalMoveDirection != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, finalMoveDirection, Time.fixedDeltaTime * 10f);
        }

        rb.velocity = new Vector3(finalMoveDirection.x * currentSpeed, rb.velocity.y, finalMoveDirection.z * currentSpeed);
        
        transform.Rotate(Vector3.up * lookInput.x * rotationSpeed * Time.fixedDeltaTime);
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
    
    public void OnJump(InputValue value)
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (value.isPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    public void OnDash(InputValue value)
    {
        isDash = value.isPressed;
    }
}