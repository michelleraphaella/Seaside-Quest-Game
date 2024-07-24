using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 5;
   

    private void Awake()
    {
        
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        
    }

    private void Swarm()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check collision with enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
        }
    }
}
