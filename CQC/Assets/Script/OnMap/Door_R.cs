using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_R : MonoBehaviour
{
    public AudioSource open;
    public AudioSource close;
    public Animator door;
    public GameObject player;
    public Transform closePos;
    private Transform playerPos;
    private float NextDoor;
    private float closingTime;
    private bool canOpen = false;
    private Vector3 speed = Vector3.zero;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        NextDoor = Time.time;
        door = gameObject.GetComponentInParent<Animator>();
        door.SetBool("isOpen_R", false);
    }
    void Update()
    {
        DoorControl();
    }
    void OnTriggerEnter(Collider col)      //부모로 만든 빈 오브젝트의 Collider를 건드리면 (누가 건드렸는지 확인)
    {
        if (col.CompareTag("Player"))
        {
            canOpen = true;
        }
    }
    void OnTriggerExit(Collider col)      //부모로 만든 빈 오브젝트의 Collider를 건드리면 (누가 건드렸는지 확인)
    {
        if (col.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
    void SetOpen()
    {
        door.SetBool("isOpen_R", true);
    }
    void SetClose()
    {
        close.Play();
        door.SetBool("isOpen_L", false);
        door.SetBool("isOpen_R", false);
        CancelInvoke("move");
    }
    void DoorControl()
    {
        if (canOpen == true && Input.GetButtonDown("use") && Time.time > NextDoor)
        {
            NextDoor = Time.time + 0.2f;
            if (door.GetBool("isOpen_R") == true && door.GetBool("isOpen_L") == false)
            {
                closingTime = 0;
                playerPos = player.transform;
                door.SetTrigger("close_R");
                InvokeRepeating("move", 0f, Time.deltaTime);
                Invoke("SetClose", 0.2f);
            }
            if (door.GetBool("isOpen_L") == true && door.GetBool("isOpen_R") == false)
            {
                door.SetTrigger("close_L");
                Invoke("SetClose", 0.2f);
            }
            if (door.GetBool("isOpen_R") == false && door.GetBool("isOpen_L") == false)
            {
                open.Play();
                door.SetTrigger("open_R");
                Invoke("SetOpen", 0.2f);
            }
        }

    }
    void move()
    {
        closingTime += Time.deltaTime;
        if (closingTime > 2f)
        {
            closingTime = 2f;
        }
        player.transform.position = Vector3.Lerp(playerPos.position, closePos.position, closingTime / 2f);
    }
}