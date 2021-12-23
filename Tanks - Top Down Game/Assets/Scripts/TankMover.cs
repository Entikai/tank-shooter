using System;
using UnityEngine;


public class TankMover : MonoBehaviour
{
    [SerializeField] private TankMovementData movementData;
    
    private Vector2 movementVector;
    private Rigidbody2D rb2D;

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
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = (Vector2)transform.up * movementVector.y * 
            currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        rb2D.MoveRotation(transform.rotation * Quaternion.Euler(
            0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}