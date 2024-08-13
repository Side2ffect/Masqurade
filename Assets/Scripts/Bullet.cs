using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveDirection;
    public float bulletSpeed;

    private void OnEnable()
    {
        Invoke(nameof(Destroy), 5.0f);
    }

    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * moveDirection);
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
