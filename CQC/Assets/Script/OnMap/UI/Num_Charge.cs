using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Num_Charge : MonoBehaviour
{
    public TMP_Text Have_Charge;
    void Start()
    {
        Have_Charge = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadAmmo();
    }
    void LoadAmmo()
    {
        Have_Charge.text = Garget.HaveCharge.ToString();
    }
}
