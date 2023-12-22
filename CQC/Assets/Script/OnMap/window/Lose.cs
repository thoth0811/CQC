using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    GameObject Player;
    public bool _open = false;
    public bool _close = false;
    public float lerptime = 0.5f;
    public float curruntTime = 0;
    public Transform open;
    public Transform close;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        LoseGame();
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
    }
    void LoseGame()
    {
        if (Player.GetComponent<Player>().health < 0 && _open == false && Check.GameStart)
        {
            if (Check.isPause == false)
            {
                Check.isPause = true;
                curruntTime = 0;
                _open = true;
            }
        }
    }
}
