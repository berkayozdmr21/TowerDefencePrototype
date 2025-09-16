using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Transform respawnPoint;
    private Renderer playerRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        
        GameObject safe2 = GameObject.Find("Safe2"); 
        if (safe2 != null)
        {
            respawnPoint = safe2.transform;
        }
        else
        {
            Debug.LogError("PlayerHealth: 'Safe2' objesi bulunamadı! Oyuncu tekrar doğamaz.");
        }

        // Karakterin Renderer bileşenini bul
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer == null)
        {
            Debug.LogWarning("Karakterde Renderer bileşeni bulunamadı. Görünmez kalabilir.");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Oyuncu hasar aldı! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            // Oyuncu ölünce geçici olarak gizle
            gameObject.SetActive(false);
            
            // Dirilme fonksiyonunu çağır
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("Oyuncu öldü, tekrar doğuyor!");
        
        // Karakteri tekrar görünür yap ve canını yenile
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Debug.Log("Oyuncu 'Safe2' noktasında tekrar doğdu.");
        }
        else
        {
            Debug.LogError("Tekrar doğma noktası (Safe2) bulunamadı. Sadece can yenilendi.");
        }
    }
}