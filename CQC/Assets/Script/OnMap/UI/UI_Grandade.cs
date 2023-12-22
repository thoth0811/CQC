using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Grandade : MonoBehaviour
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
        if(Garget.HaveGranade == 0)
        {
            button.interactable = false;
        }
    }
}
