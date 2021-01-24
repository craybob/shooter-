using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public float spawnRate;
    float nextSpawn = 0.0f;


    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        if (Time.time > nextSpawn)
        {

            nextSpawn = Time.time + spawnRate;
            Instantiate(enemy, transform.position, Quaternion.identity);



        }
    }
}
