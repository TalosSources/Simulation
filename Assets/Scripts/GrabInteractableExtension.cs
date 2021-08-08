using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractableExtension : MonoBehaviour
{
    XRGrabInteractable grab;
    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        //grab.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        //grab.throwOnDetach = false;
    }
}
