using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject loseUI, bestScoreUI;
    [SerializeField]
    private TextMeshProUGUI bestScore, currentScore;


    [Header("Debug Things")]
    public GameObject ExplosionSprite;
    public GameObject[] AsteroidLvs;
    public int livesCount;

    //aux vars
    [HideInInspector]
    public UnityEvent OnGameStop;
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
    public void PlayerDead()
    {
        if (livesCount > 0)
        {
            StartCoroutine(revivePlayer());
            livesCount--;
        }
        else
            gameStop();
    }
    private IEnumerator revivePlayer()
    {
        if (!loseUI.activeInHierarchy)
        {
            yield return new WaitForSeconds(1);
            ResourcesManager.Instance.PlayerLastPos.position = RandomPositionGenerator.Instance.RandomPos();
            ResourcesManager.Instance.PlayerLastPos.rotation = Quaternion.identity;
            ResourcesManager.Instance.PlayerLastPos.gameObject.SetActive(true);
        }
    }
    private void gameStop()
    {
        OnGameStop.Invoke();
        bestScore.text = "Best Score: " + PlayerPrefs.GetInt("BestScore").ToString();
        currentScore.text = "Points: " + ResourcesManager.Instance.PlayerPoints.ToString();

        if (ResourcesManager.Instance.PlayerPoints > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", ResourcesManager.Instance.PlayerPoints);
            bestScoreUI?.SetActive(true);
        }
        loseUI?.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
