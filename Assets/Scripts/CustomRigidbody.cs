using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public float mass;

    public Vector3 velocity;
    public Vector3 forceToAdd;

    private Rigidbody rb;

    private void Awake()
    {
        Time.fixedDeltaTime = 0.001f;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity += forceToAdd * Time.fixedDeltaTime / mass;
        forceToAdd = Vector3.zero;
        //transform.position += velocity * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }


    public void addForce(Vector3 force)
    {
        forceToAdd += force;
    }

    public void setVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
            velocity = Vector3.zero;
    }
}
