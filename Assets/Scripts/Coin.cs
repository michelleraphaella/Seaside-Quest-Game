using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float Lifetime = 10f, Life;
    public float MinValue = 0.01f, MaxValue = 0.1f;
    public float Value 
    {
        get; 
        private set; 
    }

    private Animator animator;

    void Start()
    {
        Value = Random.Range(MinValue, MaxValue);
        Life = Lifetime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Life -= Time.deltaTime;
        if (Life < 5f) animator.SetTrigger(name: "treasure die");
        if (Life < 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GameState>().Score += 1;
            Destroy(gameObject);
        }
    }
}
