using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;

    [Header("Resource UI's")]
    [SerializeField]
    private TextMeshProUGUI moneyCounter;
    [SerializeField]
    private TextMeshProUGUI pointsCounter;
    [SerializeField]
    private TextMeshProUGUI shipPrice;
    [SerializeField]
    private TextMeshProUGUI livesUI;


    [Header("Shop Things")]
    [SerializeField]
    private GameObject[] playerShips;
    [SerializeField]
    private int[] shipCosts;
    [SerializeField]
    private int lifePrice;


    //aux vars
    [HideInInspector]
    public Transform PlayerLastPos;
    [HideInInspector]
    public int PlayerPoints;
    private int playerMoney;
    private int currentPlane;

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
    private void Start()
    {
        attUI();
    }
    private void attUI()
    {
        moneyCounter.text = "$" + playerMoney.ToString();
        pointsCounter.text = "Points: " + PlayerPoints.ToString();
        shipPrice.text = "Buy Plane " +
            "$"+shipCosts[currentPlane].ToString();
        livesUI.text ="Lifes: "+ GameManager.Instance.livesCount.ToString();
    }

    public void AddPoints(int points, int money)
    {
        PlayerPoints += points;
        playerMoney += money;
        attUI();
    }

    public void BuyNewShip()
    {
        if (playerMoney >= shipCosts[currentPlane] && playerShips.Length > currentPlane + 1)
        {
            playerMoney -= shipCosts[currentPlane];
            currentPlane++;
            setShip();
            attUI();
        }
    }
    public void BuyLife()
    {
        if (playerMoney >= lifePrice)
        {
            playerMoney -= lifePrice;
            GameManager.Instance.livesCount++;
            attUI();
        }
    }
    private void setShip()
    {
        for (int i = 0; i < playerShips.Length; i++)
        {
            if (i == currentPlane)
            {
                playerShips[i].transform.position = PlayerLastPos.position;
                playerShips[i].transform.rotation = PlayerLastPos.rotation;
                playerShips[i].SetActive(true);
            }
            else
                playerShips[i].SetActive(false);
        }
    }
}
