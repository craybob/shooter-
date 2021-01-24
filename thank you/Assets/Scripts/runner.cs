using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runner : MonoBehaviour
{
    //JOYSTICK
    protected Joystick joystick;
    protected joyButton joybutton;

    private Rigidbody2D rb;
    //CHARACTERISTICS
    public float speed;
    public int pJump;
    public int damage = 10;
    public int maxHP = 10;
    public int hp;

    //GROUND DETECTOR
    private bool onGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private SpriteRenderer sprite;
    public Animator animator;


    //ATTACK 
    
    public Transform attackCheck;
    public float attackRadius;
    public LayerMask whatIsEnemy;

    private float start2attack = 2f;
    private float time2attack;


    //FACING
    private bool facingRight = true;

    //
    public GameObject[] enemyObj;
    private GameObject spawn;

    //SCORE
    public int score = 0;
    public int lvl = 0;
    //INDICATOR
    public Text hpText;
    public Text damageText;
    public Text scoreText;
    public Text highScoreText;


    //AUDIO
    public AudioClip[] player_sounds;



    // Start is called before the first frame update
    void Start()
    {
        joybutton = FindObjectOfType<joyButton>();
        joystick = FindObjectOfType<Joystick>();

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        mySoundManager.Instance.MusicPlay(player_sounds[2]);

        hp = maxHP;

        //SCORE SETUPS
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {



        run();
        destroy();
        indicators();

        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");


        

        if (joystick.Horizontal > 0 || joystick.Horizontal < 0)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
        if (joybutton.Pressed)
        {
            animator.SetBool("attack", true);
        }
        else if (!joybutton.Pressed)
        {
            animator.SetBool("attack", false);
        }

    }

    void FixedUpdate()
    {

        Attack();


        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (joystick.Vertical > 0.5f && onGround == true)
        {
            //jump();
        }

        if (facingRight == false && joystick.Horizontal > 0)
        {
            Flip();
        }
        else if ( facingRight == true && joystick.Horizontal < 0)
        {
            Flip();
        }
    }






    //FUNCTIONS

    void destroy()
    {
        if(hp <= 0)
        {
            menu_script menu;
            menu = GameObject.FindGameObjectWithTag("UI").GetComponent<menu_script>();
            menu.pause();

            Destroy(gameObject);
        }
    }



    void run()
    {
        Vector3 moveDir = new Vector3(joystick.Horizontal,joystick.Vertical).normalized;

        transform.Translate(transform.right * joystick.Horizontal * speed * Time.deltaTime);
    }



    void jump()
    {
        rb.AddForce(Vector2.up * pJump, ForceMode2D.Impulse);

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Attack()
    {
        if (Time.time >= time2attack)
        {
            if (joybutton.Pressed)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius, whatIsEnemy);

                foreach (Collider2D enemy in enemiesToDamage)
                {
                    
                    animator.SetTrigger("attack");
                    enemy.GetComponent<enemy>().TakeDamage(damage);
                }
                mySoundManager.Instance.Play(player_sounds[0]);

                time2attack = Time.time + 1f / start2attack;
            }
        }
    }


    //HEART SYSTEM
    void indicators()
    {
        hpText.text = "" + hp;
        damageText.text = "" + damage;

        scoreText.text = "" + score;

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "HIGH SCORE" + score;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("hurt");
        mySoundManager.Instance.Play(player_sounds[1]);
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }

}
