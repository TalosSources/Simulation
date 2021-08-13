using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        playerRB = GameObject.FindObjectOfType<Player>().GetComponent<Rigidbody>();
        ownCollider = GetComponent<Collider>();
    }

    public void OnShoot(InputAction.CallbackContext ctx){
        if(!grabbed.isGrabbed()) return;

        GameObject newProjectile = Instantiate(projectile.gameObject, launchingSpot);
        projectile.onLaunch(ownCollider);

        newProjectile.transform.localScale *= scaleFactor;

        Vector3 initialVelocity = transform.forward * launchingSpeed + playerRB.velocity;
        newProjectile.GetComponent<Rigidbody>().velocity = initialVelocity;
    }
}