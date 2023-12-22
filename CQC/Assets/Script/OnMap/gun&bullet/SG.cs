using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG : MonoBehaviour
{
    public int FullAmmo;
    public int Ammo;
    public int AmmoInShell;
    public float FireRate;
    public float ReloadTime;
    public float NextFire;
    public float WallDst;
    public float Set_Speed;
    public float Set_ZoomSpeed;
    public bool NowReloading;
    public bool IsHand;
    public bool IsWall;
    public GameObject Player;
    public GameObject shell;
    public GameObject bullet;//총알 가져오기
    public AudioSource shoot;//총소리 가져오기
    public AudioSource reload;//재장전 소리 가져오기
    public AudioSource pump;//펌프 소리 가져오기
    public ParticleSystem muzzle;
    public Renderer rend;
    public Collider coll;
    public Transform bulletPos;
    void Awake()
    {
        Ammo = FullAmmo;
    }
    void Start()
    {
        enable();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        WallDst = Player.GetComponent<FieldOfView>().Aim.dst;
        if (Check.isPause == false)
        {
            checkWall();
            GunShoot();
            Reload();
            Hands();
        }
    }
    void Hands()
    {
        if (Input.GetButtonDown("gun") && IsHand == false)
        {
            enable();
        }
        if (Input.GetButtonDown("handgun") && IsHand == true)
        {
            disable();
        }
    }
    public void enable()
    {
        IsHand = true;
        rend.enabled = true;
        coll.enabled = true;
        NextFire = Time.time + 0.2f;
    }
    public void disable()
    {
        IsHand = false;
        rend.enabled = false;
        coll.enabled = false;
        canelreload();
    }
    void Reload()
    {
        if (Input.GetButtonDown("reload") && Ammo != FullAmmo + 1 && NowReloading == false && IsHand == true && (Time.time > NextFire))
        {
            int needAmmo = FullAmmo - Ammo;
            NowReloading = true;
            NextFire = Time.time + ReloadTime * needAmmo;
            InvokeRepeating("DoReload", 0, ReloadTime);
            Invoke("endReload", ReloadTime * needAmmo);
        }
        if (Ammo == 0 && NowReloading == false && IsHand == true)
        {
            int needAmmo = FullAmmo - Ammo;
            NowReloading = true;
            NextFire = Time.time + ReloadTime * needAmmo + FireRate * 2;
            InvokeRepeating("DoReload", FireRate, ReloadTime);
            Invoke("endReload", FireRate + ReloadTime * needAmmo);
            Invoke("Pumping", FireRate + ReloadTime * needAmmo);
        }

    }
    void DoReload()
    {
        Ammo++;
        reload.Play();
    }
    void endReload()
    {
        CancelInvoke("DoReload");
        NowReloading = false;
    }
    void GunShoot()
    {
        if (Input.GetButtonDown("shoot") && (Time.time > NextFire) && Ammo > 0 && IsHand == true && IsWall == false && UI_Garget.isPopUp == false)
        {
            for(int i = 0; i < AmmoInShell; i++)
            {
                Instantiate(bullet, bulletPos.position, transform.rotation);//위치에 총알 복사
            }
            Invoke("Pumping", 0.1f);
            Invoke("spawnShell", 0.4f);
            shoot.Play();//총소리
            muzzle.Play();
            Ammo--;
            NextFire = Time.time + FireRate;
        }
    }
    void canelreload()
    {
        reload.Stop();
        CancelInvoke("DoReload");
        CancelInvoke("endReload");
        endReload();
    }
    void checkWall()
    {
        if (Player.GetComponent<FieldOfView>().Aim.dst < 2f)
        {
            IsWall = true;
            transform.localRotation = Quaternion.Euler(10, 0, 0);
            NextFire = Time.time + 0.4f;
        }
        else
        {
            IsWall = false;
            transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }
    void Pumping()
    {
        pump.Play();
    }
    void spawnShell()
    {
        Instantiate(shell, transform.position, transform.rotation);//위치에 탄피 복사
    }

}
