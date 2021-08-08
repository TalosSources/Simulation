using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    public GameObject other;
    void Start()
    {
        Physics.IgnoreCollision(
            GetComponent<Collider>(),
            other.GetComponent<Collider>(),
            true
            );
    }
}
