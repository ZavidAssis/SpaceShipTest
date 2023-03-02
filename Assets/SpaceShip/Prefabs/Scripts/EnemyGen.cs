using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemysToGen;
    [SerializeField]
    private int amountToGen;
    [SerializeField]
    private float timerToStart, timerToEachGen;



    private bool isFunctional;
    private void stop()
    {
        isFunctional = false;
    }
    private void Start()
    {
        GameManager.Instance.OnGameStop.AddListener(stop);
        isFunctional = true;
        StartCoroutine(generateEver());
    }

    private IEnumerator generateEver()
    {
        yield return new WaitForSeconds(timerToStart);
        while (isFunctional)
        {
            gen();
            yield return new WaitForSeconds(timerToEachGen);
        }
    }
    private void gen()
    {
        for (int i = 0; i < amountToGen; i++)
        {
            int rng = Random.Range(0, enemysToGen.Length);
            Instantiate(enemysToGen[rng], RandomPositionGenerator.Instance.RandomPos(), transform.rotation);
        }
        if (timerToEachGen > 2)
            timerToEachGen -= .2f;
    }
}
