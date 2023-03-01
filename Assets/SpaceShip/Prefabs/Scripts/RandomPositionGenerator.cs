using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{
    public static RandomPositionGenerator Instance;

    [SerializeField]
    private Vector2 fieldSize;
    private void singletonCreation()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    private void Awake()
    {
        singletonCreation();
    }
    public Vector2 RandomPos()
    {
        Vector2 pos = new Vector3(Random.Range(-fieldSize.x / 2, fieldSize.x / 2), Random.Range(-fieldSize.y / 2, fieldSize.y / 2));
        return pos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, fieldSize);
    }
}
