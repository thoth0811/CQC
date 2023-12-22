using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public bool threw = false;
    public bool isPin = true;
    public Rigidbody rigid;//����� �ٵ� ��������
    public GameObject expolde;//���� ��������
    public GameObject garget;//���� ��������
    public GameObject AR;//�� ��������
    public GameObject SG;//�� ��������
    public GameObject SR;//�� ��������
    public AudioSource pin_off;//�ɼҸ� ��������
    void Start()
    {
        garget = GameObject.FindGameObjectWithTag("Garget");
        AR = GameObject.FindGameObjectWithTag("AR");
        SG = GameObject.FindGameObjectWithTag("SG");
        SR = GameObject.FindGameObjectWithTag("SR");
        rigid = GetComponent<Rigidbody>();//����� �ٵ� �ʱ�ȭ
    }
    void Update()
    {
        granadeOnHand();
        granadeOff();
    }
    void Explode()
    {
        Instantiate(expolde, transform.position, transform.rotation);
        if(threw == false)
        {
            granadeUse();
        }
        Destroy(gameObject);//������Ʈ ����
    }
    void granadeOnHand()
    {
        if (threw == false)
        {
            transform.position = garget.transform.position;
            transform.rotation = garget.transform.rotation;
        }
        if (Input.GetButtonDown("shoot") && threw == false && isPin == true && UI_Garget.isPopUp == false)
        {
            isPin = false;
            pin_off.Play();
            Invoke("Explode", 3.5f);//2���� �����Լ� ����
        }
        if (Input.GetButtonUp("shoot") && threw == false && UI_Garget.isPopUp == false)
        {
            threw = true;
            rigid.velocity = Vector3.zero;
            rigid.AddRelativeForce(new Vector3(0f, 1.2f, -0.2f), ForceMode.Impulse);//����ź ��ô
            granadeUse();
        }
    }
    void granadeUse()
    {
        Garget.HaveGranade--;
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
        if (Input.GetButtonDown("gun") && threw == false || Input.GetButtonDown("handgun") && threw == false || Input.GetButtonDown("garget") && threw == false)
        {
            Garget.GranadeOn = false;
            Destroy(gameObject);
        }
    }
}