using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveValue : MonoBehaviour
{
    public static int diff = 1;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void diff1()
    {
        diff = 1;
    }
    public void diff2()
    {
        diff = 2;
    }
    public void diff3()
    {
        diff = 3;
    }

}
