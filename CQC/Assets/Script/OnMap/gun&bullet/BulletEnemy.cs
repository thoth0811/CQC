using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private float bulletSpeed;//총알속도
    public float bulletDamage;//총알데미지
    private float bulletSpread;
    private Rigidbody rigid;//리기드 바디 가져오기
    private void Start()
    {
        setDiff();
        rigid = GetComponent<Rigidbody>();//리기드 바디 초기화
        rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread, bulletSpread), bulletSpeed, 0f), ForceMode.VelocityChange);//속도 부여
        Destroy(gameObject, 5);//5초후 오브젝트 삭제
    }
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        Destroy(gameObject);//오브젝트 삭제
    }
    void setDiff()
    {
        bulletSpeed = 40f;
        if (SaveValue.diff == 1)
        {
            bulletDamage = 20;
            bulletSpread = 8f;
        }
        else if (SaveValue.diff == 2)
        {
            bulletDamage = 30;
            bulletSpread = 5f;
        }
        else
        {
            bulletDamage = 40;
            bulletSpread = 2f;
        }
    }
}