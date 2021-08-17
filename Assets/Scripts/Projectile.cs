using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    const float waitTime = 1f; 

    private float time = 0;
    private Collider ownCollider;
    private Collider otherCollider = null;

    private void Awake() {
        ownCollider = GetComponentInChildren<Collider>();
    }

    private void Update() {
        time += Time.deltaTime;

        if(time > waitTime){
            Physics.IgnoreCollision(ownCollider, otherCollider, false);
            //enabled = false; //Pourquoi j'ai fait ça ? (il y a surement une raison)
        }
    }

    public void onLaunch(Collider collider){
        otherCollider = collider;
        Physics.IgnoreCollision(collider, ownCollider, true);
    }
}