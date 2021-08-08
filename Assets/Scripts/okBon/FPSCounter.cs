using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float averageFramerate = 0;
    int framesCount = 0;
    void Update()
    {
        float frameRate = 1 / Time.deltaTime;
        averageFramerate = ((averageFramerate * framesCount++) + frameRate) / framesCount;
        if(framesCount % 50 == 0)
            Debug.Log("Framerate : " + frameRate);
        if (framesCount % 500 == 0)
            Debug.Log("Average Framerate : " + averageFramerate);
    }
}
