using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        transform.localPosition = new Vector3(
            cam.transform.localPosition.x,
            transform.localPosition.y,
            cam.transform.localPosition.z
            );
    }
}
