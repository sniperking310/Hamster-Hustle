using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Canvas canvas;
    private Canvas otherCanvas;
    public static SceneLoader Instance;
    public Animator transition;
    public float transitionTime = 0.5f;

    public void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
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

        CheckForCanvas();
    }

    public void Update()
    {
        CheckForCanvas();
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

    //Aktiviert sein eigenes Canvas und benutzt seinen eigenen Animator
    public void ActivateCanvas()
    {
        canvas.gameObject.SetActive(true);
        transition = GetComponentInChildren<Animator>();
        //Resetet den Animator
        transition.Rebind();
    }

    //Deaktiviert eigenes Canvas, sucht nach dem anderen Canvas und benutzt dessen Animator
    public void DeactivateCanvas()
    {
        canvas.gameObject.SetActive(false);
        otherCanvas = Object.FindAnyObjectByType<Canvas>();
        transition = otherCanvas.gameObject.GetComponentInChildren<Animator>();
        //Resetet den Animator
        transition.Rebind();
    }

    //Checkt die Anzahl der Canvases und w�hlt den richtigen Animator aus
    public void CheckForCanvas()
    {
        //Checkt die Anzahl der Canvases
        Canvas[] countCanvas = Object.FindObjectsOfType<Canvas>();
        if (countCanvas.Length < 1)
        {
            ActivateCanvas();
        }
        else if (countCanvas.Length > 1)
        {
            DeactivateCanvas();
        }
    }

    public void EndGame()
    {
        //Beendet das Spiel (nur nach build)
        Application.Quit();
    }
}
