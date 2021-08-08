using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public class Grabber : MonoBehaviour
{
    private List<Grabbed> inRange = new List<Grabbed>();
    private Grabbed grabbed = null;
    private bool justDropped;

    public Rigidbody playerRigidbody;
    public Collider armCollider;

    private void LateUpdate()
    {
        if (grabbed != null)
            grabbed.transform.position = transform.position;
        if (justDropped)
        {
            grabbed = null;
            justDropped = false;
        }
    }

    public void onGrabInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) onGrab();
        if (ctx.canceled) onRelease();
    }

    public void onGrab()
    {
        if (inRange.Count != 0)
        {
            grabbed = inRange[0];
            inRange.Remove(grabbed);
        }
    }

    public void onRelease()
    {
        if (grabbed == null) return;

        Rigidbody grabRB = grabbed.GetComponent<Rigidbody>();
        //grabbed.transform.position += transform.forward * 0.3f;
        grabbed.onRelease(armCollider);
        grabbed.transform.position = transform.position;
        grabRB.velocity = playerRigidbody.velocity;
        inRange.Add(grabbed);
        justDropped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabbed grabbed = other.GetComponent<Grabbed>();
        if (grabbed != null)
            inRange.Add(grabbed);
    }

    private void OnTriggerExit(Collider other)
    {
        Grabbed grabbed = other.GetComponent<Grabbed>();
        if (grabbed != null && inRange.Contains(grabbed))
            inRange.Remove(grabbed);
    }
}
