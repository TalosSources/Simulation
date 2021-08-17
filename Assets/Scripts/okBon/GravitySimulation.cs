using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    public AttractedObject3[] attractedObjects;
    public static AttractedObject3[] attractedObj;

    private void Awake()
    {
        Application.targetFrameRate = 80;
        Time.fixedDeltaTime = Constants.timeStep;
        attractedObj = attractedObjects;
        Grabbed.playerCollider = 
            FindObjectOfType<Player>()
            .GetComponentInChildren<Collider>();
        //Time.timeScale = 10f;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < attractedObjects.Length; ++i)
        {
            attractedObjects[i].updateVelocity(attractedObjects, Time.fixedDeltaTime);
        }

        for (int i = 0; i < attractedObjects.Length; ++i)
        {
            attractedObjects[i].updatePosition(Time.fixedDeltaTime);
        }
    }
}
