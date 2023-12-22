using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public ParticleSystem impact;
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            float impactX = col.transform.localEulerAngles.x + 90f;
            float impactY = col.transform.localEulerAngles.y;
            float impactZ = col.transform.localEulerAngles.z;
            Instantiate(impact, col.transform.position, Quaternion.Euler(impactX, impactY, impactZ));
        }
    }
}
