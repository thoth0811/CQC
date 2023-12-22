using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Camera viewCamera;
    public static Vector3 velocity;
    Vector3 wayAngle;
    float rotDegree;
    float bulletDamage;
    float root6;
    float Set_Speed;
    float Set_ZoomSpeed;
    float Set_Speed_HG;
    float Set_ZoomSpeed_HG;
    float Set_Speed_Graget = 9;
    float Speed;
    float fullHealth = 100;
    public float health;
    public bool wall_1, wall_2, wall_3;
    public int x, z;
    Vector3 mousePos;
    public ParticleSystem impact;
    public GameObject bullet, ar, sg, sr, hg;
    public LayerMask blocking , ground;
    void Start()
    {
        root6 = (float)Math.Sqrt(6f);
        rigid = transform.GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        Speed = Set_Speed;
        bulletDamage = bullet.GetComponent<BulletEnemy>().bulletDamage;
        health = fullHealth;
    }
    void Update()
    {
        setPos();
        //checkwall();
        move();
    }
    void checkwall()
    {
        Debug.DrawRay(transform.position, wayAngle * 1f , Color.green);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -45, 0) * wayAngle * 1f, Color.green);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, -45, 0) * wayAngle * 1f, Color.green);
        wall_1 = Physics.Raycast(transform.position, Quaternion.Euler(0, -45, 0) * wayAngle, 1f, blocking);
        wall_2 = Physics.Raycast(transform.position, wayAngle, 1f, blocking);
        wall_3 = Physics.Raycast(transform.position, Quaternion.Euler(0, 45, 0) * wayAngle, 1f, blocking);
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 1)
        {
            if(wall_1 == true)
            {
                x = 0;
                z = 1;
            }
            else if (wall_3 == true)
            {
                x = 1;
                z = 0;
            }
            else if(wall_2 == true)
            {
                x = 0;
                z = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 1)
        {
            if (wall_2 == true)
            {
                x = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 0)
        {
            if (wall_1 == true)
            {
                x = 1;
                z = 0;
            }
            else if (wall_3 == true)
            {
                x = 0;
                z = 1;
            }
            else if (wall_2 == true)
            {
                x = 0;
                z = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == -1)
        {
            if (wall_2 == true)
            {
                z = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == -1)
        {
            if (wall_1 == true)
            {
                x = 0;
                z = 1;
            }
            else if (wall_3 == true)
            {
                x = 1;
                z = 0;
            }
            else if (wall_2 == true)
            {
                x = 0;
                z = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == -1)
        {
            if (wall_2 == true)
            {
                x = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == 0)
        {
            if (wall_1 == true)
            {
                x = 1;
                z = 0;
            }
            else if (wall_3 == true)
            {
                x = 0;
                z = 1;
            }
            else if (wall_2 == true)
            {
                x = 0;
                z = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == 1)
        {
            if (wall_2 == true)
            {
                z = 0;
            }
        }
        else
        {
            x = 1;
            z = 1;
        }
    }
    void move()
    {
        velocity = new Vector3(wayAngle.x * x, 0, wayAngle.z * z) * Speed;
        if (Check.isPause == false)
        {
            rigid.MoveRotation(Quaternion.Euler(0, rotDegree, 0));
            //rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);
            //rigid.AddForce(velocity * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigid.velocity = velocity;
        }
    }
    void setPos()
    {
        RaycastHit hit;
        Physics.Raycast(viewCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground);
        mousePos = new Vector3(hit.point.x, 1.5f, hit.point.z);
        float dz = mousePos.z - rigid.position.z;
        float dx = mousePos.x - rigid.position.x;
        rotDegree = -(Mathf.Rad2Deg * Mathf.Atan2(dz, dx) - 90);
        wayAngle = Quaternion.Euler(0, -45, 0) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetButton("zoom"))
        {
            if (Garget.GunOn == true)
            {
                Speed = Set_ZoomSpeed;
            }
            if(Garget.HandGunOn == true)
            {
                Speed = Set_ZoomSpeed_HG;
            }
            if(Garget.GunOn == false && Garget.HandGunOn == false)
            {
                Speed = Set_Speed_Graget;
            }
        }
        else
        {
            if (Garget.GunOn == true)
            {
                Speed = Set_Speed;
            }
            if (Garget.HandGunOn == true)
            {
                Speed = Set_Speed_HG;
            }
            if (Garget.GunOn == false && Garget.HandGunOn == false)
            {
                Speed = Set_Speed_Graget;
            }
        }
    }
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            showImpact(col.gameObject);
            damage(bulletDamage);
        }
    }
    void showImpact(GameObject col)
    {
        float impactX = col.transform.localEulerAngles.x + 90f;
        float impactY = col.transform.localEulerAngles.y;
        float impactZ = col.transform.localEulerAngles.z;
        Instantiate(impact, col.transform.position, Quaternion.Euler(impactX, impactY, impactZ));
    }
    public void damage(float damage)
    {
        health -= damage;
    }
    public void SaveSpeed()
    {
        ar = transform.Find("Gun_AR").gameObject;
        sg = transform.Find("Gun_SG").gameObject;
        sr = transform.Find("Gun_SR").gameObject;
        hg = transform.Find("Gun_HG").gameObject;
        if (Check.Guntype == 1)
        {
            Set_Speed = ar.GetComponent<AR>().Set_Speed;
            Set_ZoomSpeed = ar.GetComponent<AR>().Set_ZoomSpeed;
        }
        if (Check.Guntype == 2)
        {
            Set_Speed = sg.GetComponent<SG>().Set_Speed;
            Set_ZoomSpeed = sg.GetComponent<SG>().Set_ZoomSpeed;
        }
        if (Check.Guntype == 3)
        {
            Set_Speed = sr.GetComponent<SR>().Set_Speed;
            Set_ZoomSpeed = sr.GetComponent<SR>().Set_ZoomSpeed;
        }
        Set_Speed_HG = hg.GetComponent<HG>().Set_Speed;
        Set_ZoomSpeed_HG = hg.GetComponent<HG>().Set_ZoomSpeed;
    }
}
