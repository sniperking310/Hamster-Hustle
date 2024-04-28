using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverUI;
    public Transform respawnPoint;
    
    // öffnet bei Kollision das Game-Over Interface
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            player.transform.position = respawnPoint.position;
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // startet das Level neu
    public void Restart()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }


    // leitet zum Menü weiter
    public void LoadMenu(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

}
