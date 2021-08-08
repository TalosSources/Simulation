using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttractedObject2 : MonoBehaviour
{
	public static float G = 0.0001f;

	public GameObject[] attractors;

	private Rigidbody rb;


	private void Awake()
	{
		if (attractors.Length == 0)
			attractors[0] = GameObject.FindGameObjectWithTag("Planet");

		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
	}


	void FixedUpdate()
	{
		Vector3 force = Vector3.zero;
		for (int i = 0; i < attractors.Length; ++i)
		{
			GameObject attractor = attractors[i];

			Vector3 mainVector = transform.position - attractor.transform.position;
			float distanceToEarth = mainVector.magnitude;
			mainVector.Normalize();
			Vector3 attractionVector = Vector3.zero;

			float mass = attractor.GetComponent<Rigidbody>().mass;
			float distSq = Mathf.Pow(distanceToEarth, 2);
			float factor = G * (mass * GetComponent<CustomRigidbody>().mass) / distSq;

			force += -mainVector * factor;
		}

		GetComponent<CustomRigidbody>().addForce(force);
	}
}

