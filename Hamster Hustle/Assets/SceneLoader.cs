using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Start()
    {
        //Verhindert, dass der LevelLoader bei einer neuen Scene zerstört wird
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(int index)
    {
        //Wechselt die Scene nach dem Index der Scene
        SceneManager.LoadSceneAsync(index);
    }

    public void EndGame()
    {
        //Beendet das Spiel (nur nach build)
        Application.Quit();
    }
}
