using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalObject : MonoBehaviour
{
    public AttractedObject3 planet;
    public float scaleFactor;
    public int debrisCount;
    public float velocityFactor;
    public float coolDownAge;

    public int maxDebrisCount;

    private float age = 0;


    private void Start()
    {
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    } 

    private void Update()
    {
        /*Vector3 mainVector = transform.position;
        mainVector -= planet.transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, mainVector) * transform.rotation;*/

        if(age < coolDownAge)
            age += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            //Destroy(gameObject);
        } else
        {
            if (age >= coolDownAge && collision.gameObject.CompareTag("Debris") && SatelliteSpawner.launchCount < maxDebrisCount)
            {
                for (int i = 0; i < debrisCount; ++i)
                {
                    GameObject newObject = Instantiate(gameObject, transform.parent);
                    newObject.transform.localScale *= scaleFactor;

                    Vector3 velocity = velocityFactor * SatelliteSpawner.randomVelocityWithPosition(
                        planet.transform.position, transform.position,
                        GetComponent<Rigidbody>().velocity.magnitude);

                    newObject.transform.position = transform.position;
                    newObject.GetComponent<Rigidbody>().velocity = velocity;

                    float r = Random.Range(0, 1f);
                    float g = Random.Range(0, 1f);
                    float b = Random.Range(0, 1f);
                    newObject.GetComponent<MeshRenderer>().material.color = new Color(r, g, b);

                    ++SatelliteSpawner.launchCount;
                }
                Destroy(gameObject);
            }
        }
    }
}
