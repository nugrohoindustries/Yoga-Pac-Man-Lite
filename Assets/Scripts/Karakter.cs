using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public static Karakter instance;

    private int coinCount = 0;
	public int menang = 10; 
    public GameObject gemWin;
	public GameObject gemOver;
	public Text koin;
	[Header("Player Health")]
    public int maxHealth = 3;
    public int currentHealth;
	public Text health;
	
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

	private void Start()
    {
        currentHealth = maxHealth;
    }

	public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        HitungNyawa();

        if (currentHealth <= 0)
        {
            Mokad();
        }
    }

    private void Mokad()
    {
        if (gemOver != null)
        {
            gemOver.SetActive(true);
        }
		koin.enabled = false;
		health.enabled = false;
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
		HitungKoin();
		if (coinCount >= menang && gemWin != null)
        {
            gemWin.SetActive(true);
        }
        Debug.Log("Koin: " + coinCount);
    }
	
	private void HitungKoin()
    {
        if (koin != null)
            koin.text = "Koin: " + coinCount;
    }
	
	private void HitungNyawa()
    {
        if (health != null)
            health.text = "Life: " + currentHealth;
    }
	
	
}
