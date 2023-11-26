using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 10f;
    public float speed;

    public NavMeshAgent agent;

    private void Start()
    {

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange)
        {
           
            agent.SetDestination(target.position);
            agent.speed = speed;
            transform.LookAt(target);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            
            agent.ResetPath();
        }

        
    }
    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(4);
        print("test");// Wait for 3 seconds                     
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
}
