using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public AttractedObject3 planet;
    public GameObject parent;
    public Cloud[] clouds;

    public int cloudCount;
    public float heightMin, heightMax;

    private void Start()
    {
        spawn();
    }

    void spawn()
    {
        foreach(Cloud c in clouds)
        {
            c.planet = planet;
        }

        for(int i = 0; i < cloudCount; ++i)
        {
            Vector3 position = SatelliteSpawner.randomSpherePosition(planet.transform.position, Random.Range(heightMin, heightMax));
            int cloudIndex = Random.Range(0, clouds.Length);
            GameObject newCloud = Instantiate(clouds[cloudIndex].gameObject, position, Quaternion.identity, parent.transform);
            float f = Random.Range(2f, 6);
            float g = Random.Range(2f, 6);
            newCloud.transform.localScale = new Vector3(
                newCloud.transform.localScale.x * f, 
                newCloud.transform.localScale.y, 
                newCloud.transform.localScale.z * g
                );
        }
    }
}
