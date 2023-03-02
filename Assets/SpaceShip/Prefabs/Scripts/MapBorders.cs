using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Up,
    Down,
    Left,
    Right
}

public class MapBorders : MonoBehaviour
{
    [SerializeField]
    private Side mySide;

    //aux vars
    private Collider2D col;
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mySide)
        {
            case Side.Up:
                collision.transform.position = collision.transform.position + (Vector3.down * 9);
                break;
            case Side.Down:
                collision.transform.position = collision.transform.position + (Vector3.up * 9);
                break;
            case Side.Left:
                collision.transform.position = collision.transform.position + (Vector3.right * 17);
                break;
            case Side.Right:
                collision.transform.position = collision.transform.position + (Vector3.left * 17);
                break;
        }
    }
}
