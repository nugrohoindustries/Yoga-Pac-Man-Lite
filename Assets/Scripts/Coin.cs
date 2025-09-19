using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    public int coinValue = 1;                
    public AudioClip pickupSound;            
    public GameObject pickupEffectPrefab;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Karakter.instance.AddCoins(coinValue);

           
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            
            if (pickupEffectPrefab != null)
                Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);

            
            Destroy(gameObject);
        }
    }
}
