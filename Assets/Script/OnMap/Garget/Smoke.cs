using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool threw = false;
    public bool isPin = true;
    public Rigidbody rigid;//리기드 바디 가져오기
    public GameObject expolde;//폭발 가져오기
    public GameObject garget;//가젯 가져오기
    public GameObject AR;//총 가져오기
    public GameObject SG;//총 가져오기
    public GameObject SR;//총 가져오기
    public AudioSource pin_off;//핀소리 가져오기
    void Start()
    {
        garget = GameObject.FindGameObjectWithTag("Garget");
        AR = GameObject.FindGameObjectWithTag("AR");
        SG = GameObject.FindGameObjectWithTag("SG");
        SR = GameObject.FindGameObjectWithTag("SR");
        rigid = GetComponent<Rigidbody>();//리기드 바디 초기화
    }
    void Update()
    {
        granadeOnHand();
        granadeOff();
    }
    void Explode()
    {
        Instantiate(expolde, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(0, 0, 0));
        if (threw == false)
        {
            granadeUse();
        }
        Destroy(gameObject);//오브젝트 삭제
    }
    void granadeOnHand()
    {
        if (threw == false)
        {
            transform.position = garget.transform.position;
            transform.rotation = garget.transform.rotation;
        }
        if (Input.GetButton("shoot") && threw == false && isPin == true && UI_Garget.isPopUp == false)
        {
            isPin = false;
            pin_off.Play();
            Invoke("Explode", 2f);//2초후 폭발함수 실행
        }
        if (Input.GetButtonUp("shoot") && threw == false && UI_Garget.isPopUp == false)
        {
            threw = true;
            rigid.velocity = Vector3.zero;
            rigid.AddRelativeForce(new Vector3(0f, 1.2f, -0.2f), ForceMode.Impulse);//수류탄 투척
            granadeUse();
        }
    }
    void granadeUse()
    {
        Garget.HaveSmoke--;
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
            Garget.SmokeOn = false;
            Destroy(gameObject);
        }
    }
}
