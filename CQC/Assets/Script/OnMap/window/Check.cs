using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public static bool isPause;
    public static List<GameObject> AllEnemies = new List<GameObject>();
    public static bool GameStart;
    public static int Guntype = 1;
    public int enemyNumber;
    public GameObject AR;//ÃÑ °¡Á®¿À±â
    public GameObject SG;//ÃÑ °¡Á®¿À±â
    public GameObject SR;//ÃÑ °¡Á®¿À±â
    void Start()
    {
        isPause = false;
        GameStart = false;
        AR = GameObject.FindGameObjectWithTag("Player").transform.Find("Gun_AR").gameObject;
        SG = GameObject.FindGameObjectWithTag("Player").transform.Find("Gun_SG").gameObject;
        SR = GameObject.FindGameObjectWithTag("Player").transform.Find("Gun_SR").gameObject;
        ar();
    }
    void Update()
    {
        CountEnemy();
    }
    void CountEnemy()
    {
        AllEnemies.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            AllEnemies.Add(enemy);
        }
        enemyNumber = AllEnemies.Count;
    }
    public void ar()
    {
        Guntype = 1;
        AR.SetActive(true);
        SG.SetActive(false);
        SR.SetActive(false);
    }
    public void sg()
    {
        Guntype = 2;
        AR.SetActive(false);
        SG.SetActive(true);
        SR.SetActive(false);
    }
    public void sr()
    {
        Guntype = 3;
        AR.SetActive(false);
        SG.SetActive(false);
        SR.SetActive(true);
    }
}
/*  
    void timeStop()
    {
        if(isPause == true)
        {
            Time.timeScale = 0;
        }
        if(isPause == false)
        {
            Time.timeScale = 1;
        }
    }
}
*/
