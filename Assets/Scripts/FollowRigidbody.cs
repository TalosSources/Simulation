using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody : MonoBehaviour
{
    public GameObject obj;
    private Rigidbody rb;
    private Rigidbody orb;

    private Vector3 offset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        orb = obj.GetComponentInParent<Rigidbody>();
        offset = rb.position;
    }

    void FixedUpdate()
    {
        rb.MovePosition(orb.position + offset);
        rb.MoveRotation(orb.rotation);
    }
}
