using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{

    private Rigidbody2D rb;
    private runner player;

    public float bulletSpeed;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
    }

    // Update is called once per frame
    void Update()
    {
        directionOfBullet();


    }


    void directionOfBullet()
    {
        if (transform.position.x > player.transform.position.x)
        {
            rb.velocity = new Vector2(-bulletSpeed, 0);

            transform.localScale = new Vector2(1, 1);
        }

        else if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(bulletSpeed, 0);

            transform.localScale = new Vector2(-1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player")
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
