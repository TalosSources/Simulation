using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soleilTourne : MonoBehaviour
{

    public float rotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(rotationAmount * Time.deltaTime, 0, 0));
    }
}
