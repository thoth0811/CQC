using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Charge : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Garget.HaveCharge == 0)
        {
            button.interactable = false;
        }
    }
}
