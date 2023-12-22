using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public AudioSource charge;//핀소리 가져오기
    public GameObject expolde;//폭발 가져오기
    public GameObject garget;//가젯 가져오기
    public GameObject AR;//총 가져오기
    public GameObject SG;//총 가져오기
    public GameObject SR;//총 가져오기
    public Light flash;//빛 가져오기
    public Rigidbody rigid;//리기드 바디 가져오기
    private bool HitWall = false;//벽 충돌 부울
    private bool plant = false;//설치 부울
    private float x, y, z;//설치 좌표
    private Vector3 r;//설치 방향 좌표
    void Start()
    {
        flash.enabled = false;//빛 끄기
        garget = GameObject.FindGameObjectWithTag("Garget");//총 초기화
        AR = GameObject.FindGameObjectWithTag("AR");
        SG = GameObject.FindGameObjectWithTag("SG");
        SR = GameObject.FindGameObjectWithTag("SR");
        rigid = GetComponent<Rigidbody>();//리기드 바디 초기화
    }
    void Update()
    {
        granadeOnHand();
        granadeOff();
        setPosition();
    }
    void Explode()
    {
        Instantiate(expolde, transform.position, transform.rotation);//폭발 복사
        Destroy(gameObject);//오브젝트 삭제
    }
    void granadeOnHand()
    {
        if (Input.GetButtonDown("shoot") && plant == false && HitWall == true && UI_Garget.isPopUp == false)
        {
            charge.Play();
            Invoke("Explode", 4f);//2초후 폭발함수 실행
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
    private void OnCollisionEnter(Collision col)//물체와 충돌시
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
