using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    
    public Text highscoreTxt;
    
    // Start is called before the first frame update
    void Start() {
        highscoreTxt.text = "Highscore: "+PlayerPrefs.GetInt("highscore", 0)+"\nYour Score: "+PlayerPrefs.GetInt("lastscore",0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit() {
        Application.Quit();
    }

    public void Restart() {
        SceneManager.LoadScene("TestSimon");
    }
}
