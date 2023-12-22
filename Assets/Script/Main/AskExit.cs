using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskExit : MonoBehaviour
{
    public bool windowOn = false;
    public bool _open;
    public bool _close;
    public float lerptime = 0.5f;
    public float curruntTime = 0;
    public Transform open;
    public Transform close;
    void Update()
    {
        escape();
        showExit();
    }
    void escape()
    {
        if (Input.GetButtonDown("Cancel") && _close == false && _open == false)
        {
            if (windowOn == true)
            {
                closeExit();
            }
        }
    }
    void showExit()
    {
        if (_open == true)
        {
            curruntTime += Time.deltaTime;
            if (curruntTime >= lerptime)
            {
                curruntTime = lerptime;
            }
            transform.position = Vector3.Lerp(close.position, open.position, curruntTime / lerptime);
        }
        if (_close == true)
        {
            curruntTime += Time.deltaTime;
            if (curruntTime >= lerptime)
            {
                curruntTime = lerptime;
            }
            transform.position = Vector3.Lerp(open.position, close.position, curruntTime / lerptime);
        }
    }
    public void openExit()
    {
        curruntTime = 0;
        _open = true;
        windowOn = true;
        Invoke("clearBool", lerptime);
    }
    public void closeExit()
    {
        curruntTime = 0;
        _close = true;
        windowOn = false;
        Invoke("clearBool", lerptime);
    }
    void clearBool()
    {
        _close = false;
        _open = false;
    }
}
