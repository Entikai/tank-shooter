using UnityEngine;

[CreateAssetMenu(fileName = "NewTankMovementData", menuName = "Data/Tank Movement Data", order = 1)]
public class TankMovementData : ScriptableObject
{
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    public float acceleration = 70;
    public float deacceleration = 50;
}