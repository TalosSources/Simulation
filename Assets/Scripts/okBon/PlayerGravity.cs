using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGravity : MonoBehaviour
{
    private AttractedObject3[] attractors;
    private Rigidbody rb;

    public AttractedObject3 mainAttractor;

    // Start is called before the first frame update
    void Start()
    {
        //attractors = FindObjectOfType<GravitySimulation>().attractedObjects;
        attractors = GravitySimulation.attractedObj;
        rb = GetComponent<Rigidbody>();
        mainAttractor = attractors[0];
    }

    private void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        AttractedObject3 closestAttractor = null;
        float shortestDist = float.MaxValue;

        for (int i = 0; i < attractors.Length; ++i)
        {
            AttractedObject3 attr = attractors[i];
            if (attr.Equals(this)) continue;

            Vector3 relPosition = attr.transform.position - transform.position;
            float distSq = relPosition.sqrMagnitude;
            float factor = Constants.G * attr.mass / distSq;
            acceleration += relPosition.normalized * factor;

            float distanceToSurface = attr.distanceToSurface(transform.position);
            if (distanceToSurface < shortestDist)
            {
                shortestDist = distanceToSurface;
                closestAttractor = attr;
            }
            
        }
        //Debug.Log("added acceleration " + acceleration + ", attractors length : " + attractors.Length);
        rb.AddForce(acceleration, ForceMode.Acceleration);
        mainAttractor = closestAttractor;
    }
}
