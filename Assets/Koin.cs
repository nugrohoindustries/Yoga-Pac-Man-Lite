using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            PickableManager.Instance.AddCoin();

            
            Destroy(gameObject);
        }
    }
}
