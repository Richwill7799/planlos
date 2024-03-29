﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public float maxHealth;
    public Health healthSlider;

    public Text text;
    
    float currentHealth;

    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other1)
    {
        Enemy enemy = other1.gameObject.GetComponent<Enemy>();
        //Debug.Log("Collision with: " + enemy);
        if (enemy != null)
        {
            currentHealth -= 10f;
            Destroy(enemy.gameObject);

            //Debug.Log("Where is my health?" + Health.instance);
            //Health.instance.SetValue();
            healthSlider.SetValue(currentHealth / maxHealth);
            //Debug.Log("Here is my health! " + currentHealth / maxHealth);
            if (currentHealth <= 0f) {
                GameOver();
            }
        }
    }

    public void GameOver() {
        if (PlayerPrefs.GetInt("highscore") < score) {
            PlayerPrefs.SetInt("highscore",score);
        }
        PlayerPrefs.SetInt("lastscore",score);

        SceneManager.LoadScene("GameOverScene");
    }

    public void IncreaseScore() {
        score++;
        text.text = "Score: " + score;
    }

    private void OnTriggerEnter(Collider other) {
        throw new NotImplementedException();
    }
}
