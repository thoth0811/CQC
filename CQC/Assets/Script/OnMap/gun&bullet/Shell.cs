using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public AudioSource drop;
    public Rigidbody rigid;
    void Start()
    {
        Invoke("sound", 0.5f);
        rigid.AddRelativeForce(new Vector3(0.5f, Random.Range(-0.2f, 0.2f), -0.2f), ForceMode.Impulse);
        Destroy(gameObject, 5);
    }
    void sound()
    {
        drop.Play();
    }
}
