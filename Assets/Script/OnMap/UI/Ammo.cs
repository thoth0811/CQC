using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public TMP_Text Have_Ammo;
    public GameObject player;
    public GameObject AR, SG, SR, HG;
    void Start()
    {
        Have_Ammo = GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player");
        AR = player.transform.Find("Gun_AR").gameObject;
        SG = player.transform.Find("Gun_SG").gameObject;
        SR = player.transform.Find("Gun_SR").gameObject;
        HG = player.transform.Find("Gun_HG").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        LoadAmmo();
    }
    void LoadAmmo()
    {
        if (Garget.GunOn == true)
        {
            if (Check.Guntype == 1)
            {
                Have_Ammo.text = AR.GetComponent<AR>().Ammo.ToString();
            }
            if (Check.Guntype == 2)
            {
                Have_Ammo.text = SG.GetComponent<SG>().Ammo.ToString();
            }
            if (Check.Guntype == 3)
            {
                Have_Ammo.text = SR.GetComponent<SR>().Ammo.ToString();
            }
        }
        if (Garget.HandGunOn == true)
        {
            Have_Ammo.text = HG.GetComponent<HG>().Ammo.ToString();
        }
    }
}
