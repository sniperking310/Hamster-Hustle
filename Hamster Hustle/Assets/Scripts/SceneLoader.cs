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
        //Sorgt dafür, dass es nur einen SceneLoader gibt
        if(Instance == null)
        {
            Instance = this;
            //Verhindert, dass der LevelLoader bei einer neuen Scene zerstört wird
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            //Falls es mehrere SceneLoader gibt, werden sie zerstört
            Destroy(gameObject);
        }
    }

    //Läd eine Scene mit dem index einer Scene
    IEnumerator LoadScene(int index)
    {
        //Spielt Animation für den Fade
        transition.SetTrigger("FadeOut");

        //Warte die Dauer der Animation ab
        yield return new WaitForSeconds(transitionTime);

        //Wechselt die Scene nach dem Index der Scene
        SceneManager.LoadSceneAsync(index);
    }

    //Notwendig, weil Coroutines so gestartet werden müssen
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
