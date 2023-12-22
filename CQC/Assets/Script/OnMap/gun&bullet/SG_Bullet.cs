using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Bullet : MonoBehaviour
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
        if (Garget.GunOn == true)
        rigid.AddRelativeForce(new Vector3(Random.Range(-bulletSpread / 1f, bulletSpread / 1f), bulletSpeed, 0f), ForceMode.VelocityChange);//�ӵ� �ο�
        Destroy(gameObject, 1);
    }
    private void OnCollisionEnter(Collision col)//��ü�� �浹��
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
        Destroy(gameObject);//������Ʈ ����
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
