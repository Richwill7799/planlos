using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject penguin;
    public float spawnRate = 1f;

    float timer;


    // Start is called before the first frame update
    void Start()
    {

        timer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Instantiate(penguin, transform.position, Quaternion.identity);
            timer = spawnRate; 
        }
    }
}