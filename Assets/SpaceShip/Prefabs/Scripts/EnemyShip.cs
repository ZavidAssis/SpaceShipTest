using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float mySpeed;
    [SerializeField]
    private int goldToGive, pointsToGive;

    [Header("Weapon")]
    [SerializeField]
    private GameObject myBullet;
    [SerializeField]
    private Transform[] firePoints;
    [SerializeField]
    private float fireRate;
    //aux vars
    private Rigidbody2D rb;
    private Vector2 pointToGo;

    private bool gameOn;
    private void Start()
    {
        getRefs();
        StartCoroutine(fire());
    }
    void getRefs()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOn = true;
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
    private IEnumerator fire()
    {
        while (gameOn)
        {
            yield return new WaitForSeconds(fireRate);
            SoundManager.Instance.PlaySound(SoundType.Shoot);
            foreach (Transform pos in firePoints)
                Instantiate(myBullet, pos.position, pos.rotation);
        }
    }
    public void Die()
    {
        SoundManager.Instance.PlaySound(SoundType.Explosion);
        ResourcesManager.Instance.AddPoints(pointsToGive, goldToGive);
        Instantiate(GameManager.Instance.ExplosionSprite, transform.position, transform.rotation);
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
