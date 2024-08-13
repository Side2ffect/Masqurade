using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 20;
    [SerializeField] private float startAngle;
    [SerializeField] private float endAngle;

    private Vector3 fireDirection;

    void Start()
    {
        InvokeRepeating(nameof(Fire), 0f, 2f);
    }

    private void Fire()
    {
        float angleRange = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i ++)
        {
            float bulletDirectionX = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180f);
            float bulletDirectionZ = transform.position.z + Mathf.Sin((angle * Mathf.PI) / 180f);

            fireDirection = new Vector3(bulletDirectionX, 0.0f, -bulletDirectionZ);

            GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
            bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetMoveDirection(fireDirection);

            angle += angleRange;
        }
    }
}
