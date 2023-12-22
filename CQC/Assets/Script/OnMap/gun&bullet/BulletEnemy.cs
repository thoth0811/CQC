using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private float bulletSpeed;//�Ѿ˼ӵ�
    public float bulletDamage;//�Ѿ˵�����
    private float bulletSpread;
    private Rigidbody rigid;//����� �ٵ� ��������
    private void Start()
    {
        setDiff();
        rigid = GetComponent<Rigidbody>();//����� �ٵ� �ʱ�ȭ
        rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread, bulletSpread), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
        Destroy(gameObject, 5);//5���� ������Ʈ ����
    }
    private void OnCollisionEnter(Collision col)//��ü�� �浹��
    {
        Destroy(gameObject);//������Ʈ ����
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