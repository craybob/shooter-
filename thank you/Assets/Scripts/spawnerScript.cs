using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    

    public float spawnRate;
    float nextSpawn = 0.0f;

    public Transform[] spawnPoints;

    bool spawnPer = false;

    private GameObject player;

    public GameObject[] enemy;

    private int typeOfEnemy;


    room_script roomScript;

    public float timeToDestroy = 10f;

    public Transform coordPrison;
    int typeOfRooms;
    public GameObject[] rooms;

    // Start is called before the first frame update
    void Start()
    {
        roomScript = GetComponentInParent<room_script>();
        player = GameObject.FindGameObjectWithTag("player");
    }



    // Update is called once per frame
    void Update()
    {
        


        if (Time.time > nextSpawn && spawnPer == true)
        {

            nextSpawn = Time.time + spawnRate;

            typeOfEnemy = Random.Range(0, enemy.Length);
            Instantiate(enemy[typeOfEnemy], spawnPoints[0].position, Quaternion.identity);

            typeOfEnemy = Random.Range(0, enemy.Length);
            Instantiate(enemy[typeOfEnemy], spawnPoints[1].position, Quaternion.identity);

        }

        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && spawnPer == false)
        {
            typeOfRooms = Random.RandomRange(0, rooms.Length);
            Instantiate(rooms[typeOfRooms],new Vector2(coordPrison.position.x,coordPrison.position.y + 2f),Quaternion.identity);
            spawnPer = true;
            Debug.Log("soon spawner destroy");
            Invoke("destroy", 10f);
        }
    }

    void destroy()
    {
        spawnPer = false;
        roomScript.checkPer = true;
        
        Debug.Log("bye bye");
        Destroy(gameObject);
    }


}
