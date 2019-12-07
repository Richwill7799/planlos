using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float maxHealth;

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

    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        Debug.Log("Collision with: " + enemy);
        if (enemy != null)
        {
            currentHealth -= 10f;
            Health.instance.SetValue(currentHealth / maxHealth);

            Debug.Log("It's not null!" + enemy.gameObject);
            Destroy(enemy.gameObject);
        }
    }
}
