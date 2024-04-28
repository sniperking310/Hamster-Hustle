using UnityEngine;
using UnityEngine.SceneManagement;


public class CompleteLevel1 : MonoBehaviour
{
    public GameObject player;
    public GameObject completeLevelUI;
    
    // öffnet bei Kollision das Levelende Interface
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            completeLevelUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // leitet abhängig vom Index zum Menü oder zu einer anderen Szene weiter
    public void LoadScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

}
