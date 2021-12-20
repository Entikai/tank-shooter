using System;
using UnityEngine;


public class TankMover : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb2D;
    [SerializeField] private float maxSpeed = 150;
    [SerializeField] private float rotationSpeed = 150;

    [SerializeField] private float acceleration = 70;
    [SerializeField] private float deacceleration = 50;
    private float currentSpeed = 0;
    private float currentForewardDirection = 1;

    private void Awake()
    {
        rb2D = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);

        if (movementVector.y > 0)
            currentForewardDirection = 1;
        else if (movementVector.y < 0)
            currentForewardDirection = 0;
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = (Vector2)transform.up * movementVector.y * currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}