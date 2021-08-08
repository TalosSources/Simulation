using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttractedObject3 : MonoBehaviour
{
    public float surfaceGravity;
    public float mass;
    public float radius;
    public float launchingAngle;
    public Vector3 initialVelocity;

    public AttractedObject3 mainAttractor;

    public Vector3 velocity;
    private AttractedObject3[] ignoredAttractors;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        if (radius == 0) radius = transform.lossyScale.x;
        mass = radius * radius * surfaceGravity / Constants.G;
        rb.mass = mass;
    }

    private void Start()
    {
        findMainAttractor();
        if (mainAttractor == this) return;

        velocity = findInitialVelocity();
    }

    public void updateVelocity(AttractedObject3[] objects, float delta)
    {
        for(int i = 0; i < objects.Length; ++i)
        {
            AttractedObject3 attr = objects[i];
            if (attr.Equals(this)) continue;

            Vector3 relPosition = attr.transform.position - transform.position;
            float distSq = relPosition.sqrMagnitude;
            float factor = Constants.G * attr.mass / distSq;
            Vector3 acceleration = relPosition.normalized * factor;

            updateVelocity(acceleration, delta);
        }
    }

    void updateVelocity(Vector3 acceleration, float delta)
    {
        velocity += acceleration * delta;
    }

    public void updatePosition(float delta)
    {
        //rb.position += velocity * delta;
        rb.MovePosition(rb.position + velocity * delta);
    }

    void findMainAttractor()
    {
        AttractedObject3[] attractors = FindObjectsOfType<AttractedObject3>();

        AttractedObject3 heaviest = null;
        float mass = 0;

        for(int i = 0; i < attractors.Length; ++i)
        {
            if (attractors[i].mass > mass) heaviest = attractors[i];
        }

        if (heaviest == null) throw new System.Exception();

        mainAttractor = heaviest;
    }

    public float distanceToSurface(Vector3 other)
    {
        return (other - transform.position).magnitude - radius;
    }

    Vector3 findInitialVelocity()
    {
        float speed = Constants.circularOrbitSpeed(
        (transform.position - mainAttractor.transform.position).magnitude,
        mainAttractor.mass);
        //Debug.Log("Found initial speed for " + gameObject + " : " + speed);
        Vector3 positionFromAttractor = transform.position - mainAttractor.transform.position;
        Vector3[] basis = Constants.orthogonalPlane(positionFromAttractor);
        //Debug.Log("Basis found for " + gameObject + " : {" +
        //    basis[0] + ", " + basis[1] + "}");
        float x = Mathf.Cos(launchingAngle);
        float y = Mathf.Sin(launchingAngle);
        return speed * (x * basis[0] + y * basis[1]);
    }
}
