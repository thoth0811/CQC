using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeExplode : MonoBehaviour
{
    public float smokeTime = 0;
    public Collider coll;
    void Start()
    {
        Destroy(gameObject, 22f);
    }

    // Update is called once per frame
    void Update()
    {
        smokeTime += Time.deltaTime;
    }
    void collSize()
    {

    }
}
