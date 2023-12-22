using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    GameObject diff1, diff2, diff3;
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
        showOption();
    }
    void escape()
    {
        if (Input.GetButtonDown("Cancel") && _close == false && _open == false)
        {
            if (windowOn == true)
            {
                closeOption();
            }
        }
    }
    void showOption()
    {
        if (SaveValue.diff == 1)
        {
            diff1 = GameObject.Find("diff1");
            diff1.GetComponent<Diff1>().on();
        }
        if (SaveValue.diff == 2)
        {
            diff2 = GameObject.Find("diff2");
            diff2.GetComponent<Diff2>().on();
        }
        if (SaveValue.diff == 3)
        {
            diff3 = GameObject.Find("diff3");
            diff3.GetComponent<Diff3>().on();
        }
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
    public void openOption()
    {
        curruntTime = 0;
        _open = true;
        windowOn = true;
        Invoke("clearBool", lerptime);
    }
    public void closeOption()
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
