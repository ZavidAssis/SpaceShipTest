using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int myLevel;
    [SerializeField]
    private float mySpeed;
    [SerializeField]
    private int spawnNumber;
    [SerializeField]
    private int goldToGive, xpToGive;


    //aux vars
    private Rigidbody2D rb;
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
        rb.velocity = transform.forward * mySpeed;
    }
    public void Die()
    {
        if (myLevel == 0)
            return;
        for(int i = 0; i < spawnNumber; i++)
        {
            //Instantiate(botar depois, transform.position, transform.rotation);
        }

    }
}
