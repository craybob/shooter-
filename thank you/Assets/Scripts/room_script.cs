using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_script : MonoBehaviour
{
    public bool lvlComplete;
    public Transform enemyCheck;
    public float radiusX;
    public float radiusY;
    public LayerMask whoIsEnemy;

    public bool checkPer = false;

    public GameObject room;

    private GameObject spawn;
    public enemy Enemy;

    menu_script menuScript;

    runner player_script;

    room_script[] roomScript;
    public int lvlOfRoom = 1;



    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();

        lvlOfRoom += 1;

        menuScript = GameObject.FindGameObjectWithTag("UI").GetComponent<menu_script>();

    }

    // Update is called once per frame
    void Update()
    {
        if (checkPer == true)
        {
            lvlComplete = Physics2D.OverlapBox(enemyCheck.position, new Vector2(radiusX, radiusY), 0, whoIsEnemy);
        }
        if (!lvlComplete && checkPer == true)
        {
            Debug.Log("enemy if stronger");

            spawn = GameObject.FindGameObjectWithTag("spawn");
            player_script.transform.position = spawn.transform.position;


            player_script.hp = player_script.maxHP;

            Debug.Log("soon room destroy");
            Invoke("destroy", 0.5f);
            checkPer = false;

        }


    }

    void destroy()
    {
        menuScript.lvlUpMenuStart();

        player_script.score += 1;
        player_script.lvl += 1;

        Destroy(room);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemyCheck.position, new Vector3(radiusX, radiusY , 1));
    }
}
