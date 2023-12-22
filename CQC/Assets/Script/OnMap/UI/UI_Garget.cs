using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Garget : MonoBehaviour
{
    public GameObject AR;//ÃÑ °¡Á®¿À±â
    public GameObject SG;//ÃÑ °¡Á®¿À±â
    public GameObject SR;//ÃÑ °¡Á®¿À±â
    public static bool isPopUp = false;
    public bool isPopDown = true;
    public bool firstButton = false;
    public float time;
    public float size = 0;
    void Start()
    {
        AR = GameObject.FindGameObjectWithTag("AR");
        SG = GameObject.FindGameObjectWithTag("SG");
        SR = GameObject.FindGameObjectWithTag("SR");
    }

    // Update is called once per frame
    void Update()
    {
        if(Check.GameStart && Check.isPause == false)
        {
            time += Time.deltaTime;
            transform.localScale = new Vector3(size, size, size);
            key();
            PopUp();
            PopDown();
        }
    }
    void key()
    {
        if (Input.GetButtonDown("garget"))
        {
            firstButton = true;
            if(time > 0.2f)
            {
                time = 0;
            }
            else
            {
                time = 0.2f - time;
            }
            isPopUp = true;
        }
        if (Input.GetButtonUp("garget") && isPopUp == true)
        {
            if (time > 0.2f)
            {
                time = 0;
            }
            else
            {
                time = 0.2f - time;
            }
            isPopUp = false;
        }
    }
    void PopUp()
    {
        if(isPopUp == true && firstButton == true)
        {
            if (time <= 0.2f)
            {
                size = time * 5;
            }
            else
            {
                size = 1;
            }
            isPopDown = false;
        }
    }
    void PopDown()
    {
        if(isPopUp == false && firstButton == true)
        {
            if (time <= 0.2f)
            {
                size = 1 - time * 5;
            }
            else
            {
                size = 0;
            }
            noGarget();
        }
    }
    public void _PopDown()
    {
        if (time > 0.2f)
        {
            time = 0;
        }
        else
        {
            time = 0.2f - time;
        }
        isPopUp = false;
    }
    void noGarget()
    {
        if(Garget.GranadeOn == false && Garget.StunGranadeOn == false && Garget.ChargeOn == false && Garget.SmokeOn == false && isPopDown == false)
        {
            isPopDown = true;
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
    }
}
