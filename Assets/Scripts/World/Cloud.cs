using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Cloud : MonoBehaviour
{
    public AttractedObject3 planet;
    public float speed;

    private Rigidbody rb;

    private float angle;
    private float x;
    private float y;

    //Stuff about cloud bouncing
    public float a,b;
    private float bounceDuration;
    private float time;
    private Vector3 positionReference;
        //TODO : Redo a better version with a reference and a delta, which can be bigger than the difference,
        //leading to a damped sinus wave, and of course depending of the velocity and mass of the arriving object.
            //Also do the idle slight floating oscillation

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        angle = SatelliteSpawner.randomAngle(true);
        x = Mathf.Cos(angle);
        y = Mathf.Sin(angle);  

        if(b >= 0 || a  <= 0) throw new System.Exception();
        bounceDuration = -b / a;    
        time = bounceDuration;
        positionReference = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 mainVector = transform.position - planet.transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, mainVector) * transform.rotation;
        positionReference += (x * transform.forward + y * transform.right)
                                 * Time.fixedDeltaTime
                                 * speed;
        float bounce = 0;
        if (time < bounceDuration)
        {
            time += Time.deltaTime;
            bounce = a * time * time + b * time;
        }
        transform.position = positionReference + transform.up * bounce;
    }

    private void OnCollisionEnter(Collision other) {
        if(time >= bounceDuration) time = 0;
    }
}
