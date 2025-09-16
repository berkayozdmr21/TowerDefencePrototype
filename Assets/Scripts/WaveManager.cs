using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform spawnPoint;

    public int activeEnemiesCount = 0;
    public int enemiesReachedSafe = 0;
    public int enemiesToLose = 15;
    public int tSpawnsLeft = 3;

    private bool isSpawningWave = false;
    private bool gameOverTriggered = false;

    void Start()
    {
        Time.timeScale = 1f; 
        enemiesReachedSafe = 0; 
        
        StartNewWave();
    }

    void Update()
    {
        if (gameOverTriggered)
        {
            return;
        }

        if (activeEnemiesCount == 0 && !isSpawningWave)
        {
            if (tSpawnsLeft <= 0)
            {
                EndGame();
                return;
            }
            else
            {
                StartCoroutine(StartNewWaveWithDelay(3f)); 
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && tSpawnsLeft > 0 && !isSpawningWave)
        {
            StartNewWave();
            tSpawnsLeft--;
            Debug.Log("T tuşuyla yeni grup geldi. Kalan hak: " + tSpawnsLeft);
        }

        if (enemiesReachedSafe >= enemiesToLose)
        {
            EndGame();
        }
    }

    void StartNewWave()
    {
        if (gameOverTriggered)
        {
            return;
        }

        // Dizi boş veya hiç eleman içermiyorsa kontrol et
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Düşman prefabı dizisi boş veya atanmamış!");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        
        // Bu satır, düşman prefabını güvenli bir şekilde oluşturur.
        try
        {
            GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];
            if (selectedEnemyPrefab == null)
            {
                 Debug.LogWarning("Rastgele seçilen düşman prefabı null, bu eleman atlanıyor.");
                 return;
            }
            Instantiate(selectedEnemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemiesCount++;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Düşman oluşturulurken bir hata oluştu: " + ex.Message);
            Debug.Log("Dizi Boyutu: " + enemyPrefabs.Length + ", Seçilen index: " + randomIndex);
        }
    }
    
    IEnumerator StartNewWaveWithDelay(float delay)
    {
        isSpawningWave = true;
        yield return new WaitForSeconds(delay);
        
        if (gameOverTriggered)
        {
            yield break;
        }
        
        StartNewWave();
        isSpawningWave = false;
    }

    void EndGame()
    {
        if(gameOverTriggered) return;

        Debug.Log("Oyun Bitti! Ana menüye dönülüyor.");
        
        gameOverTriggered = true;
        this.enabled = false;
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Menu");
    }
}