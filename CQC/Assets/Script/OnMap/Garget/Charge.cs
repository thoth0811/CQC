using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public AudioSource charge;//�ɼҸ� ��������
    public GameObject expolde;//���� ��������
    public GameObject garget;//���� ��������
    public GameObject AR;//�� ��������
    public GameObject SG;//�� ��������
    public GameObject SR;//�� ��������
    public Light flash;//�� ��������
    public Rigidbody rigid;//����� �ٵ� ��������
    private bool HitWall = false;//�� �浹 �ο�
    private bool plant = false;//��ġ �ο�
    private float x, y, z;//��ġ ��ǥ
    private Vector3 r;//��ġ ���� ��ǥ
    void Start()
    {
        flash.enabled = false;//�� ����
        garget = GameObject.FindGameObjectWithTag("Garget");//�� �ʱ�ȭ
        AR = GameObject.FindGameObjectWithTag("AR");
        SG = GameObject.FindGameObjectWithTag("SG");
        SR = GameObject.FindGameObjectWithTag("SR");
        rigid = GetComponent<Rigidbody>();//����� �ٵ� �ʱ�ȭ
    }
    void Update()
    {
        granadeOnHand();
        granadeOff();
        setPosition();
    }
    void Explode()
    {
        Instantiate(expolde, transform.position, transform.rotation);//���� ����
        Destroy(gameObject);//������Ʈ ����
    }
    void granadeOnHand()
    {
        if (Input.GetButtonDown("shoot") && plant == false && HitWall == true && UI_Garget.isPopUp == false)
        {
            charge.Play();
            Invoke("Explode", 4f);//2���� �����Լ� ����
            plant = true;
            granadeUse();
        }
    }
    void granadeUse()
    {
        Garget.HaveCharge--;
        Garget.key1();
        if (Check.Guntype == 1)
        {
            AR.GetComponent<AR>().enable();
        }
        if (Check.Guntype == 2)
        {
            SG.GetComponent<SG>().enable();
        }
        if (Check.Guntype == 3)
        {
            SR.GetComponent<SR>().enable();
        }
    }
    void granadeOff()
    {
        if (Input.GetButtonDown("gun") && plant == false || Input.GetButtonDown("handgun") && plant == false || Input.GetButtonDown("garget") && plant == false)
        {
            Garget.ChargeOn = false;
            Destroy(gameObject);
        }
    }
    void setPosition()
    {
        if(HitWall == true && plant == false)
        {
            if(Vector3.Distance(transform.position, new Vector3(garget.transform.position.x, garget.transform.position.y, garget.transform.position.z)) < 2.5f)
            {
                transform.position = new Vector3(x, y, z);
                transform.rotation = Quaternion.Euler(r);
                flash.enabled = true;
            }
            if(Vector3.Distance(transform.position, new Vector3(garget.transform.position.x, garget.transform.position.y, garget.transform.position.z)) >= 2.5f)
            {
                HitWall = false;
                flash.enabled = false;
            }
        }
        if (HitWall == false && plant == false)
        {
            transform.position = new Vector3(garget.transform.position.x, garget.transform.position.y, garget.transform.position.z);
            transform.rotation = garget.transform.rotation;
        }
        if(plant == true)
        {
            transform.position = new Vector3(x, y, z);
            transform.rotation = Quaternion.Euler(r);
        }
    }
    private void OnCollisionEnter(Collision col)//��ü�� �浹��
    {
        if (col.gameObject.CompareTag("wall") || col.gameObject.CompareTag("door"))
        {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
            r = transform.eulerAngles;
            HitWall = true;
        }
    }
}
