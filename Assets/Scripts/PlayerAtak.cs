using UnityEngine;

public class PlayerAtak : MonoBehaviour
{
    public float attackRate = 1f;
    public float attackRange = 60f;

    // Prefablar için yeni değişkenler
    public GameObject defaultArrowPrefab; // Default okun prefabı
    public GameObject specialArrowPrefab; // Mavi balta prefabı

    private Transform currentTarget;
    private float nextAttackTime;
    private GameObject currentArrowPrefab; // Şu anki aktif ok prefabı

    void Start()
    {
        // Oyun başladığında varsayılan ok ile başla
        currentArrowPrefab = defaultArrowPrefab;
    }

    void Update()
    {
        FindClosestEnemy();

        // E tuşuna basıldığında ok tipini değiştir
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchArrowType();
        }

        if (currentTarget != null && Time.time >= nextAttackTime)
        {
            ShootArrow();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance && distanceToEnemy <= attackRange)
            {
                closestDistance = distanceToEnemy;
                bestTarget = enemy.transform;
            }
        }
        currentTarget = bestTarget;
    }

    void ShootArrow()
    {
        // Şu anki aktif oku Instantiate et
        GameObject arrow = Instantiate(currentArrowPrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = arrow.GetComponent<Projectile>();
        if (projectileScript != null)
            projectileScript.SetTarget(currentTarget);
    }
    
    // Ok tipini değiştiren yeni fonksiyon
    void SwitchArrowType()
    {
        if (currentArrowPrefab == defaultArrowPrefab)
        {
            currentArrowPrefab = specialArrowPrefab;
            Debug.Log("Ok tipi değişti: Özel Ok!");
        }
        else
        {
            currentArrowPrefab = defaultArrowPrefab;
            Debug.Log("Ok tipi değişti: Varsayılan Ok!");
        }
    }
}