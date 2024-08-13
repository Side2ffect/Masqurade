using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Vector3 movementVector = Vector3.zero;
    public Vector3 inputVector = Vector3.zero;
    public Vector3 jumpVelocity = Vector3.zero;

    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;

    public float playerSpeed = 6.0f;
    public float walkingSpeed = 3.0f;
    public float jumpHeight = 3.5f;
    public float gravity = -20.0f;
    public float groundDistance = 0.4f;

    public bool isGrounded;
    public bool isWalking;
    public bool isJumping;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMovement();

        CheckIfGrounded();

        PlayerJump();
    }
    private void PlayerMovement()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isWalking = false;
        }

        movementVector = Vector3.ClampMagnitude(transform.right * inputVector.x + transform.forward * inputVector.z , 1.0f);

        if (isWalking)
        {
            characterController.Move(walkingSpeed * Time.deltaTime * movementVector);
        }
        else
        { 
            characterController.Move(playerSpeed * Time.deltaTime * movementVector);
            //FindObjectOfType<AudioManager>().Play("Movement");
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = -2f;
        }
    }

    private void PlayerJump()
    {
        bool isTryingJump = Input.GetKeyDown(KeyCode.Space);

        if (isTryingJump && isGrounded)
        {
            isJumping = true;
        }
        else 
        { 
            isJumping = false;
        }

        if (isJumping)
        {
            jumpVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            isGrounded = false;

            //FindObjectOfType<AudioManager>().Play("Jumping");
        }

        jumpVelocity.y += gravity * Time.deltaTime;
        characterController.Move(jumpVelocity * Time.deltaTime);
    }
}
