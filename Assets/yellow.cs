using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class yellow : MonoBehaviour
{

    public Text haigorj;
    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start();
        Input.compass.enabled = true;

        if (!Input.gyro.enabled)
            Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 verl = transform.rotation.eulerAngles;
        
        haigorj.text = Input.compass.trueHeading.ToString()
        + "\n helllo: " + nice().ToString()
        + "\n raw vector: " + Input.compass.rawVector.ToString()
        + "\n ez rawvec: " + delayedRaw().ToString()
        + "\n calculated coolness: " + scuffFix().ToString()
        ;

        Vector3 r = Input.compass.rawVector;

        Vector3 r2 = Vector3.zero;

        Quaternion threeDcompass = Quaternion.LookRotation(r,Vector3.up);

        Vector3 temp = threeDcompass.eulerAngles;

        //r2 = Quaternion.Euler(temp.);

        transform.rotation = Quaternion.Euler(verl.x, verl.y, scuffFix());
        //transform.rotation = Quaternion.Euler(0,calculatedCompass(),0);    
    }

    float scuffFix()
    {
        float coolness = Input.compass.trueHeading;

        float notcoolness = Input.gyro.gravity.y;

        return coolness + notcoolness*10;

    }

    float calculatedCompass()
    {

        Vector3 r = Input.compass.rawVector;

        r.x *= -1;
        float theta = 0;
        float atanxy = Mathf.Atan(r.x/r.y);

        if (r.y > 0 && r.x > 0)
        {
            theta = atanxy;
        }
        else if (r.y < 0)
        {
            theta = atanxy + Mathf.PI;
        }
        else
        {
            theta = atanxy + Mathf.PI*2;
        }

        return  Mathf.Rad2Deg * theta;
    }

    float cool = 0;
    float notcool = 0;
    float nice()
    {
        cool += Time.deltaTime;
        if (cool > 0.5)
        {
            cool = 0;
            notcool = Input.compass.trueHeading;
        }
        return notcool;
    }

    float cool2 = 0;
    Vector3 notcool2 = Vector3.zero;

    Vector3 delayedRaw()
    {
        cool2 += Time.deltaTime;
        if (cool2 > 0.5)
        {
            cool2 = 0;
            notcool2 = Input.compass.rawVector;
        }
        return notcool2;
    }

}
