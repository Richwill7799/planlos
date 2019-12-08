using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject penguin;
    public float spawnRate = 1f;
    public int selectSpawner;

    float lastSpawn;
    float spawnRadius = 12f;
    

    // Start is called before the first frame update
    void Start() {

        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rnd = new System.Random();
        //int selectSpawner = rnd.Next(0,2);
        Vector3 spawnPos;
        

        if (Time.time - lastSpawn >= spawnRate+Random.Range(0,20)) {
            if(selectSpawner == 0)
            {
                lastSpawn = Time.time;
                spawnPos = transform.position;
            }
            else
            {
                lastSpawn = Time.time;
                float a = Random.Range(0f, 1f);
                float b = Mathf.Sqrt(1 - Mathf.Pow(a, 2)) * rnd.Next(0, 2) * 2 - 1;
                a *= rnd.Next(0, 2) * 2 - 1;

                spawnPos = new Vector3(a * spawnRadius, b * spawnRadius, transform.position.z);
            }
            Instantiate(penguin, spawnPos, Quaternion.identity);
        }
    }
}