using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed : MonoBehaviour
{
    const float coolDown = 2f;

    public static Collider playerCollider;

    public bool trackRotation;

    float time = 0f;
    bool isCounting = false;
    Collider ownCollider;
    Collider otherCollider = null;

    public bool grabbed;

    private void Awake()
    {
        ownCollider = GetComponentInChildren<Collider>();
        grabbed = false;
    }

    private void Update()
    {
        if (isCounting)
        {
            time += Time.deltaTime;
            if(time > coolDown)
            {
                isCounting = false;
                Physics.IgnoreCollision(ownCollider, otherCollider, false);
                //Debug.Log("Resuming Collisions");
                time = 0;
            }
        }
    }
    
    public void onGrab(){
        grabbed = true;
        Physics.IgnoreCollision(ownCollider, playerCollider, true);
    }

    public void onRelease(Collider other)
    {
        grabbed = false;
        isCounting = true;
        otherCollider = other;
        Physics.IgnoreCollision(ownCollider, otherCollider, true);
        Physics.IgnoreCollision(ownCollider, playerCollider, false);
        //Debug.Log("Ignoring Collisions");
    }

    public bool isGrabbed() { return grabbed; }
}
