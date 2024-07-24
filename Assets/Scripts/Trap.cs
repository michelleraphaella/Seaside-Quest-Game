using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float Lifetime = 10f, Life;
    private Animator animator;
    [SerializeField] private int damage = 5;

    void Start()
    {
        Life = Lifetime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Life -= Time.deltaTime;
        if (Life < 5f) animator.SetTrigger(name: "die trap");
        if (Life < 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerHealth = other.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.Damage(damage);
            }
            Destroy(gameObject);
        }
    }
}
