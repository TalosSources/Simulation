using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicAttractedObject : MonoBehaviour
{
    private AttractedObject3[] attractors;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.isKinematic = false;
        attractors = GravitySimulation.attractedObj;
    }

    private void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        for (int i = 0; i < attractors.Length; ++i)
        {
            AttractedObject3 attr = attractors[i];

            Vector3 relPosition = attr.transform.position - transform.position;
            float distSq = relPosition.sqrMagnitude;
            float factor = Constants.G * attr.mass / distSq;
            acceleration += relPosition.normalized * factor;
        }
        rb.AddForce(acceleration, ForceMode.Acceleration);
    }
}
