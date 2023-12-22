using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garget : MonoBehaviour
{
    public int FullGranade;
    public int FullStunGranade;
    public int FullCharge;
    public int FullSmoke;
    public static int HaveGranade;
    public static int HaveStunGranade;
    public static int HaveCharge;
    public static int HaveSmoke;
    public static bool GunOn;
    public static bool HandGunOn;
    public static bool GranadeOn;
    public static bool StunGranadeOn;
    public static bool ChargeOn;
    public static bool SmokeOn;
    public GameObject granade;//수류탄 가져오기
    public GameObject stunGranade;//기절탄 가져오기
    public GameObject charge;//폭파장약 가져오기
    public GameObject smokeGranade;
    public GameObject garget;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            FullGranade = 1;
            FullStunGranade = 1;
            FullCharge = 1;
            FullSmoke = 1;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FullGranade = 2;
            FullStunGranade = 2;
            FullCharge = 1;
            FullSmoke = 2;
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            FullGranade = 1;
            FullStunGranade = 3;
            FullCharge = 3;
            FullSmoke = 1;
        }
        HaveGranade = FullGranade;
        HaveStunGranade = FullStunGranade;
        HaveCharge = FullCharge;
        HaveSmoke = FullSmoke;
        GunOn = true;
        HandGunOn = false;
        GranadeOn = false;
        StunGranadeOn = false;
        ChargeOn = false;
        SmokeOn = false;
    }
    void Update()
    {
        if (Check.isPause == false)
        {
            change();
        }
    }
    public static void key1()
    {
        GunOn = true;
        HandGunOn = false;
        GranadeOn = false;
        StunGranadeOn = false;
        ChargeOn = false;
        SmokeOn = false;
    }
    public static void key2()
    {
        GunOn = false;
        HandGunOn = true;
        GranadeOn = false;
        StunGranadeOn = false;
        ChargeOn = false;
        SmokeOn = false;
    }
    public static void key3()
    {
        GunOn = false;
        HandGunOn = false;
        GranadeOn = true;
        StunGranadeOn = false;
        ChargeOn = false;
        SmokeOn = false;
    }
    public static void key4()
    {
        GunOn = false;
        HandGunOn = false;
        GranadeOn = false;
        StunGranadeOn = true;
        ChargeOn = false;
        SmokeOn = false;
    }
    public static void key5()
    {
        GunOn = false;
        HandGunOn = false;
        GranadeOn = false;
        StunGranadeOn = false;
        ChargeOn = true;
        SmokeOn = false;
    }
    public static void key6()
    {
        GunOn = false;
        HandGunOn = false;
        GranadeOn = false;
        StunGranadeOn = false;
        ChargeOn = false;
        SmokeOn = true;
    }
    void change()
    {
        if (Input.GetButtonDown("gun") && GunOn == false)
        {
            key1();
        }
        if (Input.GetButtonDown("handgun") && HandGunOn == false)
        {
            key2();
        }
    }
    public void _granade()
    {
        key3();
        Instantiate(granade, transform.position, transform.rotation);
    }
    public void _stunGranade()
    {
        key4();
        Instantiate(stunGranade, transform.position, transform.rotation);
    }
    public void _smokeGranade()
    {
        key4();
        Instantiate(smokeGranade, transform.position, transform.rotation);
    }
    public void _Charge()
    {
        key6();
        Instantiate(charge, transform.position, transform.rotation);
    }
}
