using UnityEngine;

public class AimTurret : MonoBehaviour
{
    [SerializeField] private float turretRotationSpeed = 150;

    public void Aim(Vector2 inputPointorPosition)
    {
        var turretDirection = (Vector3)inputPointorPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
    }
}
