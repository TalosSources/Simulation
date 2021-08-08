using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject obj;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    void Update()
    { 
      transform.position = obj.transform.position + offset;
      transform.rotation = obj.transform.rotation;
    }
}
