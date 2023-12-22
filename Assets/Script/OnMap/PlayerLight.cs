using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    Light flash;
    void Start()
    {
        flash = GetComponent<Light>();
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            flash.spotAngle = 30;
            //transform.localPosition = new Vector3(0f, 1.5f, -6f);
            //transform.localEulerAngles = new Vector3(5, 0, 0);
        }
        else
        {
            flash.spotAngle = 70;
            //transform.localPosition = new Vector3(0f, 2f, -4f);
            //transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
