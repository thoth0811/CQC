using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HG : MonoBehaviour
{
    public int FullAmmo;
    public int Ammo;
    public float FireRate;
    public float ReloadTime;
    public float TacReloadTime;
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
    public AudioSource tacReload;//재장전 소리 가져오기
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
        disable();
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
        if (Input.GetButtonDown("gun") && IsHand == true)
        {
            disable();
        }
        if (Input.GetButtonDown("handgun") && IsHand == false)
        {
            enable();
        }
    }
    void enable()
    {
        IsHand = true;
        rend.enabled = true;
        coll.enabled = true;
        NextFire = Time.time + 0.05f;
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
        if ((Input.GetButtonDown("reload") && Ammo != FullAmmo + 1 && NowReloading == false && IsHand == true && (Time.time > NextFire)) || (Ammo == 0 && NowReloading == false && IsHand == true && (Time.time > NextFire)))
        {
            NowReloading = true;
            if (Ammo == 0)
            {
                NextFire = Time.time + ReloadTime;
                reload.Play();
                Invoke("DoReload", 1.6f);
                Invoke("endReload", 2.1f);
            }
            else
            {
                NextFire = Time.time + TacReloadTime;
                tacReload.Play();
                Invoke("DoReload", 1.6f);
                Invoke("endReload", 1.6f);
            }
        }
    }
    void DoReload()
    {
        if (Ammo == 0)
        {
            Ammo = FullAmmo;
        }
        else
        {
            Ammo = FullAmmo + 1;
        }
    }
    void endReload()
    {
        NowReloading = false;
    }
    void GunShoot()
    {
        if (Input.GetButtonDown("shoot") && (Time.time > NextFire) && Ammo > 0 && IsHand == true && UI_Garget.isPopUp == false)
        {
            Instantiate(bullet, bulletPos.position, transform.rotation);//위치에 총알 복사
            Invoke("spawnShell", 0.1f);
            shoot.Play();//총소리
            muzzle.Play();
            Ammo--;
            NextFire = Time.time + FireRate;
        }
    }
    void canelreload()
    {
        tacReload.Stop();
        reload.Stop();
        CancelInvoke("DoReload");
        CancelInvoke("endReload");
        endReload();
    }
    void checkWall()
    {
        if (Player.GetComponent<FieldOfView>().Aim.dst < 1.5f)
        {
            IsWall = true;
            transform.localRotation = Quaternion.Euler(10, 0, 0);
            NextFire = Time.time + 0.1f;
        }
        else
        {
            IsWall = false;
            transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }
    void spawnShell()
    {
        Instantiate(shell, transform.position, transform.rotation);//위치에 탄피 복사
    }

}
