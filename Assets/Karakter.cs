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
	public Text koin;
	
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
	
	
}
