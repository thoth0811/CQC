using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Num_SmokeGranade : MonoBehaviour
{
    public TMP_Text Have_SmoekGranade;
    void Start()
    {
        Have_SmoekGranade = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadAmmo();
    }
    void LoadAmmo()
    {
        Have_SmoekGranade.text = Garget.HaveSmoke.ToString();
    }
}
