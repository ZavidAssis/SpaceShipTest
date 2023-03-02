
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private LayerMask myEnemyLayer;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int timeToDestroy;





    //aux vars
    private bool canDamage;
    private Rigidbody2D rb;
    private bool moving = true;
    void Start()
    {
        StartCoroutine(getShooted());
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (moving)
            rb.velocity = transform.right * speed;
        else
            rb.velocity = Vector2.zero;
    }
    private void col()
    {
        moving = false;
        canDamage = false;
        Destroy(this.gameObject);
    }
    private IEnumerator getShooted()
    {
        moving = true;
        canDamage = true;
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((myEnemyLayer.value & (1 << collision.gameObject.layer)) > 0) && canDamage)
        {
            switch (collision.tag)
            {
                case "Player":
                    collision.gameObject.GetComponent<Player>().Die();
                    break;
                case "Enemy":
                    collision.gameObject.GetComponent<EnemyShip>().Die();
                    break;
                case "Asteroid":
                    collision.gameObject.GetComponent<Asteroid>().Die();
                    break;
            }
            col();
        }
    }
}