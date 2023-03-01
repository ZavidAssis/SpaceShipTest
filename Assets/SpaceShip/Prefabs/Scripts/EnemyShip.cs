using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float mySpeed;
    [SerializeField]
    private int goldToGive, xpToGive;


    //aux vars
    private Rigidbody2D rb;
    private Vector2 pointToGo;


    private void Start()
    {
        getRefs();
    }
    void getRefs()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        movement();
    }


    private void movement()
    {
        if (Vector2.Distance(transform.position, pointToGo) <= .3f)
            pointToGo = RandomPositionGenerator.Instance.RandomPos();
        else
        {
            Vector2 dir = (pointToGo - (Vector2)transform.position).normalized;
            rb.velocity = dir * mySpeed;
            setOrientation((Vector2)transform.position + dir);
        }
    }
    private void setOrientation(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotate, 10 * Time.deltaTime);
    }
    private void fire()
    {

    }
    public void Die()
    {

    }
}
