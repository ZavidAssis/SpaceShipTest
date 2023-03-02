using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float boostSpeed;
    [SerializeField]
    private float friction;

    [Header("Weapon")]
    [SerializeField]
    private GameObject myBullet;
    [SerializeField]
    private Transform[] firePoints;
    [SerializeField]
    private float fireRate;


    //aux vars
    private Rigidbody2D rb;
    private float cdShoot;
    void Start()
    {
        getRefs();

    }
    private IEnumerator invencibleTime()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<Collider2D>().enabled = true;
    }
    private void getRefs()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rotate(Input.GetAxis("Horizontal"));
        boost(Input.GetAxis("Vertical"));


        if (cdShoot > 0)
            cdShoot -= Time.fixedDeltaTime;
        else if (Input.GetAxisRaw("Fire1") == 1)
        {
            fire();
            cdShoot = fireRate;
        }
    }

    //rotaciona o player
    private void rotate(float axis)
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + (rotationSpeed * Time.fixedDeltaTime * -axis));
        transform.rotation = Quaternion.Euler(rot);
    }
    //acelera para frente
    private void boost(float axis)
    {
        rb.AddForce(transform.right * boostSpeed * axis, ForceMode2D.Force);
        rb.AddForce((-new Vector3(rb.velocity.x, rb.velocity.y) * friction) * Time.fixedDeltaTime);
    }
    //metodo para atirar
    private void fire()
    {
        SoundManager.Instance.PlaySound(SoundType.Shoot);
        foreach (Transform pos in firePoints)
            Instantiate(myBullet, pos.position, pos.rotation);
    }

    public void Die()
    {
        SoundManager.Instance.PlaySound(SoundType.Lose);
        GameManager.Instance.PlayerDead();
        this.gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        ResourcesManager.Instance.PlayerLastPos = this.transform;
    }
}
