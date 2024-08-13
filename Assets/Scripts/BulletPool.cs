using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

    [SerializeField] private GameObject poolBullet;
    private readonly bool tooManyBullets = true;

    private List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstance = this;
    }

    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i ++)
            {
                if(!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (tooManyBullets)
        {
            GameObject bullet = Instantiate(poolBullet);
            bullet.SetActive(false);
            bullets.Add(bullet);
            return bullet;
        }

        return null;
    }
}
