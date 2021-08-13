using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    public OrbitalObject satellite;
    public AttractedObject3 planet;
    public float heightFactor;
    private float initialSpeed;

    public float timeBetwwenLaunches = 0.1f;

    public static int launchCount = 0;
    public int maxLaunchCount;

    private float time = 0;

    private void Start()
    {
        if (planet == null) planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<AttractedObject3>();
        satellite.planet = planet;
        initialSpeed = Constants.circularOrbitSpeed(planet.radius * heightFactor ,planet.mass);
    }

    // Update is called once per frame
    void Update()
    {
        if (launchCount > maxLaunchCount) gameObject.SetActive(false);

        time += Time.deltaTime;

        if (time > timeBetwwenLaunches)
        {
            GameObject newSatellite = Instantiate(satellite.gameObject);
            Vector3 position = randomSpherePosition(planet.transform.position, 
                heightFactor * planet.transform.lossyScale.x);
            Vector3 direction = randomVelocityWithPosition(planet.transform.position, position, initialSpeed);

            newSatellite.transform.position = position;
            Rigidbody rb = newSatellite.GetComponent<Rigidbody>();
            rb.velocity = direction;

            ++launchCount;
            time = 0;
        }
    }

    public static Vector3 randomSpherePosition(Vector3 center, float height)
    {
        //Randomizing the position
        float longitude = randomAngle(true);
        float z = Random.Range(-1f, 1f);
        float a = Mathf.Sqrt(1f - Mathf.Pow(z, 2));
        float x = a * Mathf.Cos(longitude);
        float y = a * Mathf.Sin(longitude);

        Vector3 planetNormal = height * new Vector3(x, y, z);
        return planetNormal + center;
    }

    public static Vector3 randomVelocityWithPosition(Vector3 center, Vector3 position, float initialSpeed)
    {
        Vector3 normal = position - center;
        //Randomizing the launching rotation
        float launchingAngle = randomAngle(true);
        Vector3[] basis = orthogonalPlane(normal);

        return initialSpeed * (Mathf.Cos(launchingAngle) * basis[0] + Mathf.Sin(launchingAngle) * basis[1]);
    }

    public static float randomAngle(bool two_pi)
    {
        return Random.Range(0, (two_pi ? 2 : 1) * Mathf.PI);
    }

    public static Vector3[] orthogonalPlane(Vector3 normal)
    {
        if (normal == Vector3.zero) throw new System.Exception();

        float n1 = normal.x;
        float n2 = normal.y;
        float n3 = normal.z;

        Vector3[] ret = new Vector3[2];

        if(n1 != 0)
        {
            Vector3 x = new Vector3(n2, -n1, 0);
            Vector3 y = new Vector3(n3, 0, -n1);

            ret[0] = x;
            ret[1] = y - Vector3.Project(y, x);
            ret[1].Normalize();
        } else
        {
            ret[0] = n2 == 0 ?
                ret[0] = new Vector3(0, 1, 0) :
                ret[0] = new Vector3(0, n3, -n2);
            ret[1] = new Vector3(1, 0, 0);
        }
        ret[0].Normalize();

        return ret;
    }

    private void OnDestroy()
    {
        launchCount = 0;
    }
}
