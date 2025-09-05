using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Settings")]
    public float duration = 5f;

    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            StartCoroutine(ActivatePowerUp(other.gameObject));
        }
    }

    private System.Collections.IEnumerator ActivatePowerUp(GameObject player)
    {
        isActive = true;

        
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();

        
        foreach (EnemyAI enemy in enemies)
        {
            enemy.SetPlayerPowerUp(true);
        }

       
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        
        foreach (EnemyAI enemy in enemies)
        {
            enemy.SetPlayerPowerUp(false);
        }

        Destroy(gameObject);
    }
}
