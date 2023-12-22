using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diff2 : MonoBehaviour
{
    Toggle diff;
    void Start()
    {
        diff = GetComponent<Toggle>();
    }
    public void on()
    {
        diff.isOn = true;
    }
}
