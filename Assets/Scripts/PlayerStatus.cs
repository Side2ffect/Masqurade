using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int startHp;
    public int currentHp;

    public float InvulnerableTime;
    public float blockDamage;

    public CameraShake cameraShake;

    void Start()
    {
        currentHp = startHp;
    }

    void Update()
    {
        if (InvulnerableTime >= 0)
        {
            InvulnerableTime -= Time.deltaTime;
        }
    }

    public void GetInvulnerable()
    {
        InvulnerableTime = 2.0f;
    }

    public void LostInvulnerable()
    {
        InvulnerableTime = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (InvulnerableTime <= 0)
            {
                Destroy(other);

                currentHp -= 1;
                print(currentHp);
                InvulnerableTime = blockDamage;

                //FindObjectOfType<AudioManager>().Play("DamageTaken");

                StartCoroutine(cameraShake.Shake(0.15f, 0.7f));
            }
        }
    }
}
