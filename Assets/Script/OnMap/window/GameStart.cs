using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    GameObject player;
    public float lerptime = 0.5f;
    public float curruntTime = 0;
    public Transform open;
    public Transform close;
    bool startClick = false;
    bool setClose = false;
    bool _setStart = false;
    void Start()
    {
    }
    void Update()
    {
        openWindow();
        closeWindow();
    }
    void openWindow()
    {
        if (startClick == false)
        {
            Check.isPause = true;
            curruntTime += Time.deltaTime;
            if (curruntTime >= lerptime)
            {
                curruntTime = lerptime;
            }
            transform.position = Vector3.Lerp(close.position, open.position, curruntTime / lerptime);
        }
    }
    void closeWindow()
    {
        if(setClose == true)
        {
            curruntTime += Time.deltaTime;
            if (curruntTime >= lerptime)
            {
                curruntTime = lerptime;
            }
            transform.position = Vector3.Lerp(open.position, close.position, curruntTime / lerptime);
            if(_setStart == false)
            {
                Invoke("setStart", lerptime);
            }
        }
    }
    public void startclick()
    {
        curruntTime = 0;
        startClick = true;
        setClose = true;
    }
    void setStart()
    {
        _setStart = true;
        Check.GameStart = true;
        Check.isPause = false;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().SaveSpeed();
    }
}
