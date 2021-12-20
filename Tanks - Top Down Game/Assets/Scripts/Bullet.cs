using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] private int damage = 5;
    [SerializeField] private float maxDistance = 10;

    private Vector2 startPosition;
    private float conqueredDistance = 0;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        startPosition = transform.position;
        rb2D.velocity = transform.up * speed;
    }

    private void Update()
    {
        conqueredDistance = Vector2.Distance(transform.position, startPosition);

        if (conqueredDistance >= maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(damage);
        }
        DisableObject();
    }
}