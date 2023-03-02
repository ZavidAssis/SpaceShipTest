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


    //aux vars
    private Rigidbody2D rb;
    void Start()
    {
        getRefs();
    }
    private void getRefs()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rotate(Input.GetAxis("Horizontal"));
        boost(Input.GetAxis("Vertical"));
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
