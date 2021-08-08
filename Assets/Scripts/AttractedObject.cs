using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttractedObject : MonoBehaviour
{
	public static float G = 0.0001f;

	public GameObject[] attractors;
	public float drag;
	public float orbitTreshold;

	[HideInInspector]
	public GameObject mainAttractor;

	private Rigidbody rb;


    private void Awake()
    {
		if (attractors.Length == 0)
			attractors[0] = GameObject.FindGameObjectWithTag("Planet");

		mainAttractor = attractors[0];
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		float earthRadius = mainAttractor.transform.lossyScale.z;
		orbitTreshold = earthRadius * 100;
	}


	void FixedUpdate()
	{
		Vector3 force = Vector3.zero;
		for(int i = 0; i < attractors.Length; ++i)
        {
			GameObject attractor = attractors[i];

			Vector3 mainVector = transform.position - attractor.transform.position;
			float distanceToEarth = mainVector.magnitude;
			mainVector.Normalize();
			Vector3 attractionVector = Vector3.zero;

			float mass = attractor.GetComponent<Rigidbody>().mass;
			float distSq = Mathf.Pow(distanceToEarth, 2);
			float factor =  G * (mass * rb.mass) / distSq;

			if (distanceToEarth < orbitTreshold)
			{
				attractionVector = -mainVector * factor;
			}

			Vector3 dragForce = -rb.velocity * drag;
			force += attractionVector + dragForce;
		}

		rb.AddForce(force);
	}

	public GameObject[] attractorr()
    {
		return attractors;
    }
}
