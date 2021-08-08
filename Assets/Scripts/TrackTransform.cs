using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        transform.position = obj.transform.position;
        //transform.rotation = obj.transform.rotation;
        //transform.localScale = obj.transform.localScale;
    }
}
