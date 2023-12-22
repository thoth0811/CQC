using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCamera : MonoBehaviour
{
    public Camera cameraMain;
    public Transform player;//Ÿ�� ��������
    public Transform zoom;//Ÿ�� ��������
    float root6;
    void Start()
    {
        root6 = (float)Math.Sqrt(6f);
        cameraMain = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
        zoom = GameObject.Find("zoomCam").transform;
        cameraMain.orthographicSize = 15f;
        transform.position = new Vector3(zoom.position.x + 5 * root6, 30f, zoom.position.z - 5 * root6);
    }
    void LateUpdate()
    {
        if (Check.isPause == false)
        {
            zoomUp();
        }
    }
    void zoomUp()
    {
        if (Input.GetButton("zoom"))
        {
            transform.position = new Vector3(zoom.position.x + 5 * root6, 30f, zoom.position.z - 5 * root6);//Ÿ�� ���󰡱�
        }
        else
        {
            transform.position = new Vector3(player.position.x + 5 * root6, 30f, player.position.z - 5 * root6);//Ÿ�� ���󰡱�
        }
    }
}
