using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Bullet : MonoBehaviour
{
    private float bulletSpeed;//총알속도
    private float bulletDamage;//총알데미지
    private float bulletSpread;
    public GameObject expolde;
    private Rigidbody rigid;//리기드 바디 가져오기
    private void Start()
    {
        setDiff();
        rigid = GetComponent<Rigidbody>();//리기드 바디 초기화
        if (Garget.GunOn == true)
        rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 1f, bulletSpread / 1f), bulletSpeed, 0f), ForceMode.VelocityChange);//속도 부여
        Destroy(gameObject, 1);
    }
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        if(col.gameObject.CompareTag("enemy"))
        {
            GameObject enemy = col.gameObject;
            enemy.GetComponent<Enemy>().damage(bulletDamage);
        } 
        if(col.gameObject.CompareTag("door"))
        {
            Instantiate(expolde, transform.position, transform.rotation);
        }    
        Destroy(gameObject);//오브젝트 삭제
    }
    void setDiff()
    {
        bulletSpeed = 25f;
        if (SaveValue.diff == 1)
        {
            bulletDamage = 10f;
            bulletSpread = 3f;
        }
        else if (SaveValue.diff == 2)
        {
            bulletDamage = 8f;
            bulletSpread = 3.5f;
        }
        else
        {
            bulletDamage = 6f;
            bulletSpread = 4f;
        }
    }
}
