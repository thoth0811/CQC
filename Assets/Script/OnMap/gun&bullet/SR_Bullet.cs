using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Bullet : MonoBehaviour
{
    private float bulletSpeed;//�Ѿ˼ӵ�
    private float bulletDamage;//�Ѿ˵�����
    private float bulletSpread;
    public GameObject expolde;
    private Rigidbody rigid;//����� �ٵ� ��������
    private void Start()
    {
        setDiff();
        rigid = GetComponent<Rigidbody>();//����� �ٵ� �ʱ�ȭ
        if (Player.velocity != Vector3.zero)
        {
            bulletSpread = bulletSpread * 3;
        }
        if (Input.GetButton("zoom"))
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 1f, bulletSpread / 1f), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
        }
        else
        {
            rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread * 10f, bulletSpread * 10f), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
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
        if (col.gameObject.CompareTag("door"))
        {
            Instantiate(expolde, transform.position, transform.rotation);
        }
        Destroy(gameObject);//������Ʈ ����
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
