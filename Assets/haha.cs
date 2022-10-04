using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haha : MonoBehaviour
{


    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var funny = Input.compass.rawVector;
        transform.rotation = Quaternion.Euler(0,funny.y,0);
            //transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private Quaternion GyroToUnity (Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
