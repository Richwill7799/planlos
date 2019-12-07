using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject penguin;
    public float spawnRate = 1f;

    float lastSpawn;


    // Start is called before the first frame update
    void Start() {

        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastSpawn >= spawnRate+Random.Range(0,7)) {
            lastSpawn = Time.time;
            Instantiate(penguin, transform.position, Quaternion.identity);
        }
    }
}