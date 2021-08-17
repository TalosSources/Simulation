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

    private Player player;
    private Grabber rightGrabber;
    private Grabber leftGrabber;

    private void Awake() {
        grabbed = GetComponent<Grabbed>();
        player = FindObjectOfType<Player>();
        playerRB = player.GetComponent<Rigidbody>();
        ownCollider = GetComponentInChildren<Collider>();

        rightGrabber = player.rightArm.GetComponent<Grabber>();
        leftGrabber = player.leftArm.GetComponent<Grabber>();
    }

    public void OnShoot(InputAction.CallbackContext ctx){
        if(!grabbed.isGrabbed() || !ctx.performed) return;

        GameObject newProjectile = Instantiate(projectile.gameObject);
        newProjectile.transform.position = launchingSpot.transform.position;
        newProjectile.transform.rotation = launchingSpot.transform.rotation;
        newProjectile.GetComponent<Projectile>().onLaunch(ownCollider);

        newProjectile.transform.localScale *= scaleFactor;

        Vector3 initialVelocity = transform.forward * launchingSpeed + playerRB.velocity;
        newProjectile.transform.position += initialVelocity * Time.deltaTime;
        newProjectile.GetComponent<Rigidbody>().velocity = initialVelocity;
    }

    /*public void OnShootRight(InputAction.CallbackContext ctx)
    {
        if (rightGrabber.grabbed == grabbed) OnShoot(ctx);
    }

    public void OnShootLeft(InputAction.CallbackContext ctx)
    {
        if (leftGrabber.grabbed == grabbed) OnShoot(ctx);
    }*/
}