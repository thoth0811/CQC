using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    void Start()
    {
        Instantiate(enemy, transform.position, transform.rotation);//위치에 적 복사
        Destroy(gameObject);
    }
}
