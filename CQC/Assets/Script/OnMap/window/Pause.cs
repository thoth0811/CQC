using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool _open = false;
    public bool _close = false;
    public float lerptime = 0.5f;
    public float curruntTime = 0;
    public Transform open;
    public Transform close;
    void Update()
    {
        PauseButton();
        showPause();
    }
    void showPause()
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
    void PauseButton()
    {
        if(Input.GetButtonDown("Cancel") && _close == false && _open == false && Check.GameStart == true)
        {
            if(Check.isPause == false)
            {
                openPause();
                Check.isPause = true;
            }
            else if(Check.isPause == true)
            {
                closePause();
                Check.isPause = false;
            }
        }
    }
    public void openPause()
    {
        curruntTime = 0;
        _open = true;
        Invoke("clearBool", lerptime);
    }
    public void closePause()
    {
        curruntTime = 0;
        _close = true;
        Invoke("clearBool", lerptime);
    }
    void clearBool()
    {
        _close = false;
        _open = false;
    }
}
