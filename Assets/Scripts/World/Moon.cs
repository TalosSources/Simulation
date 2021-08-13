using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public float heightFactor;
    public float speed;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject planet = GetComponent<AttractedObject2>().attractors[0];

        //transform.position = new Vector3(0, heightFactor * planet.transform.lossyScale.y, 0) + planet.transform.position;

        float orbitAngle = Mathf.PI / 3;
        float x = Mathf.Cos(orbitAngle);
        float y = Mathf.Sin(orbitAngle);

        Vector3[] basis = SatelliteSpawner.orthogonalPlane(transform.position - planet.transform.position);
        //GetComponent<Rigidbody>().velocity = (x * basis[0] + y * basis[1]) * speed;
        //GetComponent<CustomRigidbody>().setVelocity((x * basis[0] + y * basis[1]) * speed);
    }

    private void FixedUpdate()
    {
        Vector3 mainVector = transform.position - 
            GetComponent<AttractedObject2>().attractors[0].transform.position;
       // transform.rotation = 
           // Quaternion.FromToRotation(transform.up, mainVector) * transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touché la Lune !");
        AttractedObject3 comp = other.gameObject.GetComponent<AttractedObject3>();
        if (comp != null)
        {
            Debug.Log("Attiré par la Lune !");
            comp.mainAttractor = gameObject.GetComponent<AttractedObject3>().mainAttractor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Quitté La Lune !");
        AttractedObject comp = other.gameObject.GetComponent<AttractedObject>();
        if (comp != null)
        {
            comp.mainAttractor = GameObject.FindGameObjectWithTag("Planet");
        }
    }

    private bool contains(GameObject[] objects, GameObject obj)
    {
        for(int i = 0; i < objects.Length; ++i)
        {
            if (objects[i] == obj) return true;
        }
        return false;
    }
}
