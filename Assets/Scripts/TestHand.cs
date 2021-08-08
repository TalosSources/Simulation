using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestHand : MonoBehaviour
{
    private bool lastValue = false;
    public Collider playerCollider;
    public void onSelectEnter(SelectEnterEventArgs args)
    {
        Collider other = args.interactable.GetComponent<Collider>();
        lastValue = Physics.GetIgnoreCollision(playerCollider, other);
        Physics.IgnoreCollision(playerCollider, other, true);
    }

    public void onSelectExit(SelectExitEventArgs args)
    {
        Rigidbody grab = args.interactable.gameObject.GetComponent<Rigidbody>();
        //if (grab != null) grab.velocity = playerCollider.gameObject.GetComponentInParent<Rigidbody>().velocity;
        //else Debug.Log("grab est nul ptn");
        Debug.Log("Dropped Item ! Player speed : " +
            playerCollider.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude +
            ", item speed : " + grab.velocity.magnitude);
        Collider other = args.interactable.GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, other, lastValue);
    }

}
