using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableManager : MonoBehaviour
{
    public static PickableManager Instance;

    [Header("UI")]
    public Text coinText;

    private int coinCount = 0;
	public GameObject menangGame; 
    public int totalCoin = 10;

    private void Awake()
    {
       
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateUI();
		if (coinCount >= totalCoin)
        {
            Menang();
        }
    }
	
	private void Menang()
    {
        if (menangGame != null)
            menangGame.SetActive(true);
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Koin: " + coinCount.ToString();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}