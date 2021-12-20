using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    [SerializeField] private List<Transform> barrels;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float reloadDelay = 1;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField] private int booletPoolCount = 10;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>(); //Can I refactor this script so it doesn't know about the Object Pooler.
    }

    private void Start()
    {
        bulletPool.Initialize(bulletPrefab, booletPoolCount);
    }

    private void Update()
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }
    public void Shoot()
    {
        if (canShoot == true)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            foreach (Transform barrel in barrels)
            {
                GameObject bulletInstance = bulletPool.CrateObject();
                bulletInstance.transform.position = barrel.position;
                bulletInstance.transform.localRotation = barrel.rotation;
                bulletInstance.GetComponent<Bullet>().Initialize();

                foreach (Collider2D tankCollider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), tankCollider);
                }
            }
        }
    }
}