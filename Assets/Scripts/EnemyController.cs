using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxCopiesToSpawn = 2;
    public float circleRadius = 5f; 
    public int circleSegments = 8;

    private bool isCopy = false;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    
    private WaveManager waveManager;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        
        GameObject safe1 = GameObject.Find("Safe");
        GameObject safe2 = GameObject.Find("Safe2");

        if (safe1 != null)
        {
            waypoints.Add(safe1.transform);
            waypoints.AddRange(CreateCircleWaypoints(safe1.transform));
        }

        if (safe2 != null)
        {
            waypoints.Add(safe2.transform);
        }

        if (waypoints.Count == 0)
        {
            Debug.LogError("Yol noktaları (Safe ve Safe2) bulunamadı! Düşmanlar hareket edemeyecek.");
        }
        
        if (!isCopy)
        {
            int copiesToSpawn = UnityEngine.Random.Range(0, maxCopiesToSpawn + 1);
            for (int i = 0; i < copiesToSpawn; i++)
            {
                Vector3 spawnPosition = transform.position;
                float randomX = UnityEngine.Random.Range(-18f, 18f);
                float randomZ = UnityEngine.Random.Range(-18f, 18f);
                if (Mathf.Abs(randomX) < 9f) randomX = (randomX > 0) ? 9f : -9f;
                if (Mathf.Abs(randomZ) < 9f) randomZ = (randomZ > 0) ? 9f : -9f;
                Vector3 offset = new Vector3(randomX, 0f, randomZ);
                spawnPosition += offset;
                
                GameObject newEnemy = Instantiate(gameObject, spawnPosition, transform.rotation);
                EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
                if (newEnemyController != null)
                {
                    newEnemyController.isCopy = true;
                }
                
                if(waveManager != null)
                {
                    waveManager.activeEnemiesCount++;
                }
            }
        }
    }

    void Update()
    {
        if (waypoints.Count == 0)
        {
            return;
        }

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        Vector3 enemyPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPosXZ = new Vector3(targetPosition.x, 0, targetPosition.z);
        
        if (Vector3.Distance(enemyPosXZ, targetPosXZ) < 0.1f)
        {
            if (currentWaypointIndex >= waypoints.Count - 1)
            {
                Debug.Log("Düşman Safe noktasına ulaştı ve kayboldu.");

                if(waveManager != null)
                {
                    waveManager.enemiesReachedSafe++;
                    waveManager.activeEnemiesCount--;
                }
                Destroy(gameObject);
                return;
            }

            if (waypoints[currentWaypointIndex].name == "Safe")
            {
                if (waveManager != null)
                {
                    waveManager.enemiesReachedSafe++;
                }
            }
            currentWaypointIndex++;
        }
    }

    private List<Transform> CreateCircleWaypoints(Transform center)
    {
        List<Transform> circlePoints = new List<Transform>();
        for (int i = 0; i < circleSegments; i++)
        {
            float angle = i * 360f / circleSegments;
            float x = center.position.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = center.position.z + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            GameObject point = new GameObject("CirclePoint" + i);
            point.transform.position = new Vector3(x, center.position.y, z);
            circlePoints.Add(point.transform);
        }
        return circlePoints;
    }
}