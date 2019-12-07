using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public float maxHealth;
    public Health healthSlider;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        //Debug.Log("Collision with: " + enemy);
        if (enemy != null)
        {
            currentHealth -= 10f;
            Destroy(enemy.gameObject);

            Debug.Log("Where is my health?" + Health.instance);
            //Health.instance.SetValue();
            healthSlider.SetValue(currentHealth / maxHealth);
            Debug.Log("Here is my health!" + Health.instance);
        }
    }
}
