using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HP_Bar;
    public GameObject player;
    public Player HP;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        HP = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HP_Bar.value = HP.health;
    }
}
