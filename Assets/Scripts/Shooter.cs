using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private Grabber grabber;

    private void Awake()
    {
        grabber = GetComponentInChildren<Grabber>();
        if (grabber == null) throw new System.Exception("Shooter must have a grabber component");
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        InstanceGun gun = grabber.grabbed.GetComponent<InstanceGun>();
        if (gun != null)
        {
            gun.OnShoot(ctx);
        }
    }
}
