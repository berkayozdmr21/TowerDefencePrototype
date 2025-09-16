using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Hangi sahneye geçeceğimizi belirleyen değişken
   
    public string gameSceneName = "GameScene";

    void Update()
    {
        // Eğer kullanıcı SPACE tuşuna basarsa
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        // GameScene sahnesini yükle
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
        // Uygulamadan çıkış yap
        Application.Quit();
        
        // Eğer Unity Editor'deysek
        Debug.Log("Oyundan çıkıldı.");
    }
}