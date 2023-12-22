using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Num_StunGranade : MonoBehaviour
{
    public TMP_Text Have_StunGranade;
    void Start()
    {
        Have_StunGranade = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadAmmo();
    }
    void LoadAmmo()
    {
        Have_StunGranade.text = Garget.HaveStunGranade.ToString();
    }
}