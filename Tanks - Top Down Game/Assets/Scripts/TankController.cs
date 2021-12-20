using System.Collections.Generic;
using UnityEngine;


public class TankController : MonoBehaviour
{
    private AimTurret aimTurret;
    private TankMover tankMover;
    private Turret[] turrets;

    private void Awake()
    {
        if (tankMover == null)
            tankMover = GetComponentInChildren<TankMover>();

        if (aimTurret == null)
            aimTurret = GetComponentInChildren<AimTurret>();

        if (turrets == null || turrets.Length == 0)
        {
            turrets = GetComponentsInChildren<Turret>();
        }
    }

    public void HandleShoot()
    {
        foreach (Turret turret in turrets)
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        tankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);
    }
}