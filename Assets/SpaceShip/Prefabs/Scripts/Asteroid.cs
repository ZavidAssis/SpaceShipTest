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
    private int goldToGive, pointsToGive;


    //aux vars
    private Rigidbody2D rb;
    private void Start()
    {
        getRefs();
        randomizeRot();
    }
    private void randomizeRot()
    {
        int rngRot = Random.Range(0, 4);
        Quaternion rot = Quaternion.Euler(0, 0, rngRot * 90);
        transform.rotation = rot;
    }
    void getRefs()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * mySpeed;
    }
    public void Die()
    {
        SoundManager.Instance.PlaySound(SoundType.Explosion);
        ResourcesManager.Instance.AddPoints(pointsToGive, goldToGive);
        if (myLevel == 0)
        {
            Instantiate(GameManager.Instance.ExplosionSprite, transform.position, transform.rotation);
        }
        else
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                int rngRot = Random.Range(0, 4);
                Quaternion rot = Quaternion.Euler(0, 0, rngRot * 90);
                Instantiate(GameManager.Instance.AsteroidLvs[myLevel - 1], transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.Die();
            Die();
        }
    }
}
