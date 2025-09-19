using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; 

public class Musuh : MonoBehaviour
{
    public float roamRadius = 10f;
    public float roamDelay = 3f;
    public string playerTag = "Player";
    public GameObject gemover;
    private NavMeshAgent agent;
    private float roamTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamTimer = roamDelay;
    }

    void Update()
    {
        roamTimer += Time.deltaTime;

        if (roamTimer >= roamDelay)
        {
            Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
            agent.SetDestination(newPos);
            roamTimer = 0;
        }
    }

    
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Game Over!");
            gemover.SetActive(true);
        }
    }
}
