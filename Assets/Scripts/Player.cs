using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private GameObject damageIndicatorPrefab;
    private List<GameObject> activeIndicators = new List<GameObject>();
    private float currentHealth;
    [SerializeField] private Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private IEnumerator ShowDamageIndicator()
    {
        GameObject indicator = Instantiate(damageIndicatorPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        activeIndicators.Add(indicator); // Add the indicator to the list

        SpriteRenderer indicatorRenderer = indicator.GetComponent<SpriteRenderer>();

        // Optional: Add a fade-out effect
        Color originalColor = indicatorRenderer.color;
        float duration = 1.0f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            indicatorRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f - (t / duration));
            indicator.transform.position += new Vector3(0, Time.deltaTime, 0); // Move up over time
            yield return null;
        }

        activeIndicators.Remove(indicator); // Remove the indicator from the list
        Destroy(indicator);
    }

    public void Damage(float amount)
    {
        this.currentHealth -= amount;
        StartCoroutine(ShowDamageIndicator());
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check collision with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameObject.CompareTag("Player"))
            {
                if (gameObject != null)
                {
                    Debug.Log("damage");
                    Damage(5);
                }
            }
        }
    }

    private void Die()
    {
        // Destroy all active indicators
        foreach (GameObject indicator in activeIndicators)
        {
            Destroy(indicator);
        }
        activeIndicators.Clear();

        if (GameState.Instance != null)
        {
            GameState.Instance.GameOver();
        }

        if (healthbar != null && healthbar.gameObject != null)
        {
            Destroy(healthbar.gameObject);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Keyboard.current.dKey.wasPressedThisFrame)
        //{
        //    if (gameObject != null)
        //    {
        //        Debug.Log("damage");
        //        Damage(10);
        //    }
        //}
    }
}
