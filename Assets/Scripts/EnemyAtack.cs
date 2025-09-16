using UnityEngine;

public class EnemyAtak : MonoBehaviour
{
    public float attackRate = 1f; // Saniyede 1 ok
    public float attackRange = 100f; // Düşmanın ok atacağı menzil
    public GameObject arrowPrefab; // Atılacak ok prefabı

    private Transform playerTarget;
    private float nextAttackTime;

    void Start()
    {
        // "Player" etiketine sahip objeyi bul
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            Debug.LogError("EnemyAtak: 'Player' etiketiyle bir obje bulunamadı!");
        }
    }

    void Update()
    {
        // Eğer oyuncu hedef olarak ayarlanmışsa
        if (playerTarget != null)
        {
            // Oyuncuya olan mesafeyi hesapla
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            // Eğer oyuncu menzil içindeyse ve ok atma zamanı geldiyse
            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    ShootArrow();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void ShootArrow()
    {
        // Ok prefabını oluştur
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        
        // EnemyProjectile scriptine oyuncu hedefini ata
        EnemyProjectile projectileScript = arrow.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(playerTarget);
        }
    }
}