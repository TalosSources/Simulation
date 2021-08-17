using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(EstimateVelocity))]
public class Grabber : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Collider armCollider;

    private List<Grabbed> inRange = new List<Grabbed>();

    [HideInInspector]
    public Grabbed grabbed = null;

    private bool justDropped;
    private float handOffsetFactor = 0.2f;

    private EstimateVelocity ev;

    private void Awake()
    {
        ev = GetComponent<EstimateVelocity>();
        //ev.local = false;
        ev.local = true;
    }

    private void LateUpdate()
    {
        if (grabbed != null)
        {
            grabbed.transform.position = transform.position;
            if (grabbed.trackRotation)
                grabbed.transform.rotation = transform.rotation;
            else
                grabbed.transform.position += (playerRigidbody.transform.up + transform.forward) * handOffsetFactor;
        }

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
        /*if (inRange.Count != 0)
        {
            grabbed = inRange[0];
            grabbed.onGrab();
            //inRange.Remove(grabbed);
        }*/

        foreach(Grabbed g in inRange)
        {
            if (!g.grabbed)
            {
                grabbed = g;
                g.onGrab();
                break;
            }
        }
    }

    public void onRelease()
    {
        if (grabbed == null) return;

        Rigidbody grabRB = grabbed.GetComponent<Rigidbody>();

        grabbed.onRelease(armCollider);
        grabbed.transform.position = transform.position + 
            (playerRigidbody.transform.up + transform.forward) * handOffsetFactor;

        grabRB.velocity = playerRigidbody.velocity + ev.velocity;

        justDropped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Grabbed grabbed = other.GetComponentInParent<Grabbed>();
        if (grabbed != null)
            inRange.Add(grabbed);
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has exited the collider");
        Grabbed grabbed = other.GetComponent<Grabbed>();
        if (grabbed != null && inRange.Contains(grabbed))
        {
            //Debug.Log("it has been removed");
            //Debug.Log("In range : " + print(inRange));
            inRange.Remove(grabbed);
        }
    }

    private string print(List<Grabbed> list)
    {
        string s = "[";
        for(int i = 0; i < list.Count - 1; ++i)
        {
            s += list[i].gameObject.name + ", "; 
        }
        return s + list[list.Count - 1] + "]";
    }
}
