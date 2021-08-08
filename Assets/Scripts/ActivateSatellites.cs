using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSatellites : MonoBehaviour
{
    public GameObject obj;
    private GameObject cur = null;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision !");

        if (!collider.gameObject.CompareTag("Arm")) return;

        if (cur == null) cur = Instantiate(obj);
        else { 
            Destroy(cur);
            cur = null;
        }
    }

}

