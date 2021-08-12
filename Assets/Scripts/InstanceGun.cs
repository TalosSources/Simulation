using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a faire encore peut etre : sliders (diégétiques) pour la taille et la vitesse

public class InstanceGun : MonoBehaviour
{
    public Projectile projectile;
    public Transform launchingSpot;

    public float launchingSpeed;
    public float scaleFactor; 

    private Grabbed grabbed;
    private Rigidbody playerRB;
    private Collider ownCollider;

    private void Awake() {
        grabbed = GetComponent<Grabbed>();
        playerRB = GameObject.FindObjectOfType<player>().GetComponent<Rigidbody>();
        ownCollider = GetComponent<Collider>();
    }

    public OnShoot(InputAction.CallbackContext ctx){
        if(!grabbed.isGrabbed) return;

        GameObject newProjectile = Instantiate(projectile.gameObject, launchingSpot.position);
        projectile.onLaunch();

        newProjectile.localScale *= scaleFactor;

        Vector3 initialVelocity = transform.forward * launchingSpeed + playerRB.velocity;
        newProjectile.GetComponent<Rigidbody>().velocity = initialVelocity;
    }
}