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
    //A very good idea could be to make the cloud a basic attracted object, and add to its rigidbody the exact opposite to the force it would get from the planet at the desired height.
    //That way, the cloud would naturally goes toward that height.

    //sticking the player
    private Player player = null;
    private Vector3 lastPosition;

    //New floating stuff
    public float pressionFactor;
    private float standardHeight;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        //rb.interpolation = RigidbodyInterpolation.Interpolate;
        angle = SatelliteSpawner.randomAngle(true);
        x = Mathf.Cos(angle);
        y = Mathf.Sin(angle);

        speed = Random.Range(0, speed);

        if(b >= 0 || a  <= 0) throw new System.Exception();
        bounceDuration = -b / a;    
        time = bounceDuration;
        positionReference = transform.localPosition;
        lastPosition = transform.position;

        standardHeight = (transform.position - planet.transform.position).sqrMagnitude;
    }

    private void FixedUpdate()
    {
        Vector3 mainVector = transform.position - planet.transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, mainVector) * transform.rotation;

        //--------------------------------------------------------------

        positionReference += (x * transform.forward + y * transform.right)
                                 * Time.fixedDeltaTime
                                 * speed;
        float bounce = 0;
        if (time < bounceDuration)
        {
            time += Time.deltaTime;
            bounce = a * time * time + b * time;
        }
        transform.localPosition = positionReference + transform.up * bounce;

        if(player != null)
        {
            Vector3 delta = transform.position - lastPosition;
            player.transform.position += delta;
        }
        lastPosition = transform.position;

        //--------------------------------------------------------------

        transform.position += (x * transform.forward + y * transform.right)
                                 * Time.fixedDeltaTime
                                 * speed;
        //float delta2 = standardHeight - mainVector.sqrMagnitude;
        //Vector3 force = delta2 * mainVector.normalized;
        //rb.AddForce(force * pressionFactor, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other) {
        if(time >= bounceDuration) time = 0;
        if (other.gameObject.tag == "Player") player = other.gameObject.GetComponent<Player>();
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player") player = null;
    }
}
