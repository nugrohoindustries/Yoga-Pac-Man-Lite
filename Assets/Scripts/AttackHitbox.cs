using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Karakter ph = other.GetComponent<Karakter>();
            if (ph != null)
            {
                ph.TakeDamage(1);
            }
        }
    }
}
