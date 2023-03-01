using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float timeTo;
    private void Start()
    {
        Destroy(gameObject, timeTo);
    }
}
