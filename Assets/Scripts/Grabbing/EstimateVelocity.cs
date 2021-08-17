using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstimateVelocity : MonoBehaviour
{
    public bool local;

    public Vector3 velocity;
    public Vector3 debugVelocity;

    private Vector3 pastPosition;
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
        pastPosition = local ? (transform.position - parent.position) : transform.position;
    }

    void Update()
    {
        Vector3 localPosition = transform.position - parent.position;
        Vector3 delta = (local ? localPosition : transform.position) - pastPosition;
        debugVelocity = delta / Time.deltaTime;
        //velocity = transform.TransformVector(debugVelocity);
        velocity = debugVelocity;
        pastPosition = local ? localPosition : transform.position;
    }
}
