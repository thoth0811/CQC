using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
     public void main()
     {
        SceneManager.LoadScene(0);
     }
     public void loadmap1()
     {
        SceneManager.LoadScene(1);
     }
     public void loadmap2()
     {
        SceneManager.LoadScene(2);
     }
     public void loadmap3()
     {
        SceneManager.LoadScene(3);
     }
}
