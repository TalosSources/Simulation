using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static float G = 0.01f;
    public static float timeStep = 0.01f;

    public static float circularOrbitSpeed(float distance, float mass)
    {
        return Mathf.Sqrt( (G * mass) / distance);
    }

    public static Vector3[] orthogonalPlane(Vector3 normal)
    {
        if (normal == Vector3.zero) throw new System.Exception();

        float n1 = normal.x;
        float n2 = normal.y;
        float n3 = normal.z;

        Vector3[] ret = new Vector3[2];

        if (n1 != 0)
        {
            Vector3 x = new Vector3(n2, -n1, 0);
            Vector3 y = new Vector3(n3, 0, -n1);

            ret[0] = x;
            ret[1] = y - Vector3.Project(y, x);
            ret[1].Normalize();
        }
        else
        {
            ret[0] = n2 == 0 ?
                ret[0] = new Vector3(0, 1, 0) :
                ret[0] = new Vector3(0, n3, -n2);
            ret[1] = new Vector3(1, 0, 0);
        }
        ret[0].Normalize();

        return ret;
    }
}
