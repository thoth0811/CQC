using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Bullet : MonoBehaviour
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
        if (Player.velocity != Vector3.zero)
        {
            bulletSpread = bulletSpread * 3;
        }
        if (Input.GetButton("zoom"))
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 1f, bulletSpread / 1f), bulletSpeed, 0f), ForceMode.VelocityChange);//속도 부여
        }
        else
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread * 10f, bulletSpread * 10f), bulletSpeed, 0f), ForceMode.VelocityChange);//속도 부여
        }
        Destroy(gameObject, 2);
    }
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            GameObject enemy = col.gameObject;
            enemy.GetComponent<Enemy>().damage(bulletDamage);
        }
        if (col.gameObject.CompareTag("door"))
        {
            Instantiate(expolde, transform.position, transform.rotation);
        }
        Destroy(gameObject);//오브젝트 삭제
    }
    void setDiff()
    {
        bulletSpeed = 60f;
        if (SaveValue.diff == 1)
        {
            bulletDamage = 100;
            bulletSpread = 1f;
        }
        else if (SaveValue.diff == 2)
        {
            bulletDamage = 80;
            bulletSpread = 1.5f;
        }
        else
        {
            bulletDamage = 80;
            bulletSpread = 2f;
        }
    }
}
