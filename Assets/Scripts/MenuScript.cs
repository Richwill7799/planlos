using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
       
        Application.Quit();
        
    }
}
