using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Bullet : MonoBehaviour
{
    private float bulletSpeed;//�Ѿ˼ӵ�
    private float bulletDamage;//�Ѿ˵�����
    private float bulletSpread;
    private Rigidbody rigid;//����� �ٵ� ��������
    private void Start()
    {
        setDiff();
        rigid = GetComponent<Rigidbody>();//����� �ٵ� �ʱ�ȭ
        if(Player.velocity != Vector3.zero )
        {
            bulletSpread = bulletSpread * 3;
        }
        if (Input.GetButton("zoom"))
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 2, bulletSpread / 2), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
        }
        else
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 1f, bulletSpread / 1f), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
        }
        Destroy(gameObject, 2);
    }
    private void OnCollisionEnter(Collision col)//��ü�� �浹��
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            GameObject enemy = col.gameObject;
            enemy.GetComponent<Enemy>().damage(bulletDamage);
        }
        Destroy(gameObject);//������Ʈ ����
    }
    void setDiff()
    {
        bulletSpeed = 40f;
        if (SaveValue.diff == 1)
        {
            bulletDamage = 40;
            bulletSpread = 2f;
        }
        else if (SaveValue.diff == 2)
        {
            bulletDamage = 35;
            bulletSpread = 3f;
        }
        else
        {
            bulletDamage = 30;
            bulletSpread = 4f;
        }
    }
}
