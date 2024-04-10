using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader Instance;
    public Animator transition;
    public float transitionTime = 0.5f;

    public void Awake()
    {
        //Sorgt daf�r, dass es nur einen SceneLoader gibt
        if(Instance == null)
        {
            Instance = this;
            //Verhindert, dass der LevelLoader bei einer neuen Scene zerst�rt wird
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            //Falls es mehrere SceneLoader gibt, werden sie zerst�rt
            Destroy(gameObject);
        }
    }

    //L�d eine Scene mit dem index einer Scene
    IEnumerator LoadScene(int index)
    {
        //Spielt Animation f�r den Fade
        transition.SetTrigger("FadeOut");

        //Warte die Dauer der Animation ab
        yield return new WaitForSeconds(transitionTime);

        //Wechselt die Scene nach dem Index der Scene
        SceneManager.LoadSceneAsync(index);
    }

    //Notwendig, weil Coroutines so gestartet werden m�ssen
    public void StartLoadScene(int index) 
    {
        StartCoroutine(LoadScene(index));
    }

    public void EndGame()
    {
        //Beendet das Spiel (nur nach build)
        Application.Quit();
    }
}
