using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damageAmount = 10f;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 2D çarpışmaları algılamak için OnTriggerEnter2D kullanın
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}