using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Num_Granade : MonoBehaviour
{
    public TMP_Text Have_Granade;
    void Start()
    {
        Have_Granade = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadAmmo();
    }
    void LoadAmmo()
    {
        Have_Granade.text = Garget.HaveGranade.ToString();
    }
}
