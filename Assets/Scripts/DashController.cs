using UnityEngine;

public class DashController : MonoBehaviour
{
    public bool isDashing = false;
    public bool isSuperJumping = false;

    private float dashStart;

    PlayerController playerController;
    CharacterController characterController;
    PlayerStatus playerStatus;

    [SerializeField] ParticleSystem forwardDashParticleSystem;
    [SerializeField] ParticleSystem backwardDashParticleSystem;
    [SerializeField] ParticleSystem leftDashParticleSystem;
    [SerializeField] ParticleSystem rightDashParticleSystem;
    [SerializeField] ParticleSystem superjumpParticleSystem;

    private float dashCooldown = 1.0f;
    private float lastTimeSuperJump = 0.0f;
    private readonly float superjumpHeight = 6.0f;
    private readonly float superjumpDelay = 0.2f;
    private float superjumpCooldown = 2.5f;

    public Camera cam;
    public Transform currentPoint;
    public GameObject projectile;
    public float shootForce = 0.3f;

    public bool canShoot = true;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        HandleDash();
        HandleSuperJump();
    }

    #region Dash
    void HandleDash()
    {
        bool isTryingDash = Input.GetKeyDown(KeyCode.LeftShift);

        if (isTryingDash && !isDashing)
        {
            if(dashCooldown <= 0.0f)
            {
                OnStartDash();
            }
        }

        if (isDashing)
        {
            if (Time.time - dashStart <= 0.4f)
            {
                if(playerController.movementVector.Equals(Vector3.zero))
                {
                    characterController.Move(30f * Time.deltaTime * transform.forward);
                }
                else
                {
                    characterController.Move(30f * Time.deltaTime * playerController.movementVector.normalized);
                }
            }
            else
            {
                OnEndDash();
            }
        }

        dashCooldown -= Time.deltaTime;
    }

    void OnStartDash()
    {
        isDashing = true;
        dashStart = Time.time;

        PlayDashParticles();
        //FindObjectOfType<AudioManager>().Play("Dashing");

        playerStatus.GetInvulnerable();
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStart = 0;
        dashCooldown = 1.0f;

        playerStatus.LostInvulnerable();

        canShoot = true;
    }
    #endregion

    void PlayDashParticles()
    {
        Vector3 inputVector = playerController.inputVector;

        if (inputVector.z > 0 && Mathf.Abs(inputVector.x) <= inputVector.z)
        {
            forwardDashParticleSystem.Play();
            return;
        }

        if (inputVector.z < 0 && Mathf.Abs(inputVector.x) <= Mathf.Abs(inputVector.z))
        {
            backwardDashParticleSystem.Play();
            return;
        }

        if (inputVector.x < 0)
        {
            leftDashParticleSystem.Play();
            return;
        }

        if (inputVector.x > 0)
        {
            rightDashParticleSystem.Play();
            return;
        }

        forwardDashParticleSystem.Play();
    }

    #region SuperJump
    void HandleSuperJump()
    {
        bool isTryingSuperJump = Input.GetKeyDown(KeyCode.Q);

        if ((Time.time - lastTimeSuperJump) < superjumpDelay)
        {
            if (isSuperJumping)
            {
                OnEndSuperJump();
            }
            return;
        }
        
        if (isTryingSuperJump && superjumpCooldown <= 0.0f)
        {
            OnStartSuperJump();
            SuperJump();
        }

        superjumpCooldown -= Time.deltaTime;
    }

    void SuperJump()
    {
        if (!playerController.isGrounded)
        {
            playerController.jumpVelocity.y = Mathf.Sqrt((superjumpHeight / 1.5f) * -2f * playerController.gravity);
        }
        else
        {
            playerController.jumpVelocity.y = Mathf.Sqrt(superjumpHeight * -2f * playerController.gravity);
        }
    }

    void OnStartSuperJump()
    {
        isSuperJumping = true;
        lastTimeSuperJump = Time.time;

        //FindObjectOfType<AudioManager>().Play("SuperJumping");

        superjumpParticleSystem.Play();
    }

    void OnEndSuperJump()
    {
        isSuperJumping = false;
        superjumpCooldown = 2.5f;
    }

    #endregion

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if(isDashing && canShoot)
            {
                Shoot();
                canShoot = false;
            }
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        Vector3 targetPoint = ray.GetPoint(100);
        Vector3 targetDirection = targetPoint - currentPoint.position;

        GameObject dashBullet = Instantiate(projectile, currentPoint.position, Quaternion.identity);
        dashBullet.transform.forward = targetDirection.normalized;

        dashBullet.GetComponent<Rigidbody>().AddForce(targetDirection.normalized * shootForce, ForceMode.Impulse);

        //FindObjectOfType<AudioManager>().Play("Deflecting");
    }
}
