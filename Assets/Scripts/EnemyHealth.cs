using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float blinkDuration = 0.2f;
    private Renderer enemyRenderer;
    private Color originalColor;
    
    private WaveManager waveManager;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
        
        waveManager = FindObjectOfType<WaveManager>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Düşman hasar aldı! Kalan can: " + currentHealth);

        if (enemyRenderer != null)
        {
            StartCoroutine(BlinkEffect());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Düşman öldü!");
        
        // Düşman öldüğünde WaveManager'daki sayacı düşür
        if (waveManager != null)
        {
            waveManager.activeEnemiesCount--;
        }
        
        Destroy(gameObject);
    }
    
    IEnumerator BlinkEffect()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(blinkDuration);
        enemyRenderer.material.color = originalColor;
    }
}