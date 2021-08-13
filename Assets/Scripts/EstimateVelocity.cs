using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstimateVelocity : MonoBehaviour
{
    public bool local;

    public Vector3 velocity;

    private Vector3 pastPosition;

    private void Start()
    {
        pastPosition = local ? transform.localPosition : transform.position;
    }

    void Update()
    {
        Vector3 delta = (local ? transform.localPosition : transform.position) - pastPosition;
        velocity = delta / Time.deltaTime;
        pastPosition = local ? transform.localPosition : transform.position;
    }
}
