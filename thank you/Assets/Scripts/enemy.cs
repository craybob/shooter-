using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int typeOfEnemy;

    //MAIN CHARACTERISTICS
    public int hp;
    public float speed;
    public int damage;

    //COMPONENTS
    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer sprite;

    //ATTACK
    public Transform attackCheck;
    public float attackRadius;
    public LayerMask whatIsPlayer;

    public float start2attack;
    public float time2attack;

    private bool attack;

    //ATTACK WIZARD
    public GameObject magicBullet;

    //FOR MOVEMENT
    public float stoppingDistance;
    private runner player;
    enemy Enemy;

    //FACING
    private bool facingRight = false;


    //SOUND EFFECT AND MUSIC

    //SCORE MANAGEMENT
    scoreManager scoreManagerScript;

    //faf
    room_script roomScript;
    public bool lvlUp;

    // Start is called before the first frame update



    void Start()
    {

        Enemy = GetComponent<enemy>();
        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("player").GetComponent<scoreManager>();

        hp += player.lvl;
        damage += player.lvl;



        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (typeOfEnemy == 1)
        {
            AttackBandit();
        }

        else if (typeOfEnemy == 2)
        {
            AttackWizard();
        }


        die();



        if (  Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
        {
            moveToPlayer();
        }
        else if(Vector2.Distance(transform.position, player.transform.position) < stoppingDistance)
        {
            stopToMove();
        }
    }


    void moveToPlayer()
    {

        anim.SetBool("move", true);


        if (typeOfEnemy == 1)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);

                transform.localScale = new Vector2(-2, 2);
            }


            else if (transform.position.x > player.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);

                transform.localScale = new Vector2(2, 2);

            }
        }
        if(typeOfEnemy == 2)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);

                transform.localScale = new Vector2(0.75f, 0.75f);
            }


            else if (transform.position.x > player.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);

                transform.localScale = new Vector2(-0.75f, 0.75f);

            }
        }


    }

    void stopToMove()
    {
        anim.SetBool("move", false);
        rb.velocity = new Vector2(0, 0);
    }


   
    

    public void TakeDamage(int damage)
    {

        if (typeOfEnemy == 1)
        {
            hp -= damage;
            anim.SetTrigger("hurt");
        }
        else if (typeOfEnemy == 2)
        {
            hp -= damage;
        }
    }

    void die()
    {
        if (hp <= 0)
        {
            
            if (typeOfEnemy == 1)
            {
                anim.SetBool("dead", true);
                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;
                speed = 0;
                Invoke("Destroy", 2f);
            }
            else if (typeOfEnemy == 2)
            {
                Destroy();
            }
        }
    }

    void Destroy()
    {
        scoreManagerScript.score += 1;
        Destroy(gameObject);
    }

    void AttackBandit()
    {
        if (time2attack <= 0)
        {
            if (attack == true)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius, whatIsPlayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    runner tempEnemy = enemiesToDamage[i].GetComponent<runner>();
                    if (tempEnemy)
                    {
                        anim.SetTrigger("attack");
                        tempEnemy.TakeDamage(damage);
                        attack = false;
                    }
                }
                time2attack = start2attack;
            }
            attack = true;
        }
        else
        {
            time2attack -= Time.deltaTime;
        }
    }

    void AttackWizard()
    {


        if (time2attack <= 0)
        {
            if (attack == true)
            {


                if (transform.position.x < player.transform.position.x)
                {
                    anim.SetTrigger("attack");

                    Instantiate(magicBullet, attackCheck.position, Quaternion.identity);

                    transform.localScale = new Vector2(0.75f, 0.75f);
                }


                else if (transform.position.x > player.transform.position.x)
                {
                    anim.SetTrigger("attack");

                    Instantiate(magicBullet, attackCheck.position, Quaternion.identity);

                    transform.localScale = new Vector2(-0.75f, 0.75f);

                }

                attack = false;
                time2attack = start2attack;

            }
            time2attack = start2attack;
            attack = true;
        }
        else
        {
            time2attack -= Time.deltaTime;
        }
    }

    public void lvlUpEnemy(int lvlOfRoom)
    {
        hp += lvlOfRoom;
        damage += lvlOfRoom;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }
}
