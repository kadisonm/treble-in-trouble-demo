using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    public string scene;

    private void Awake() 
    { 
        _instance = this; 
        SceneManager.activeSceneChanged += NewScene;
        scene = SceneManager.GetActiveScene().name;
    } 

    private void NewScene(Scene old, Scene current)
    {
        SceneTransition transition = FindFirstObjectByType<SceneTransition>();

        float delay = 0f;

        if (current.name == "01_Menu") {
            delay = 6;
        }

        if (transition)
        {
            StartCoroutine(transition.FadeIn(3f, delay));
        }
    }

    private void ChangeScene(string name)
    {
        SceneTransition transition = FindFirstObjectByType<SceneTransition>();

        if (transition)
        {
            StartCoroutine(transition.FadeOut(3, 0, () => {
                SceneManager.LoadScene(name);
            }));
        }
    }

    private IEnumerator Scene0Dialogue()
    {
        DialogueManager.Instance.CreateSpeech("Dad", "Caden! Caden! You must wake up now!", 3);
        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateSpeech("Dad", "Now, Caden!", 3);
        yield return new WaitForSecondsRealtime(6);
        ChangeScene("02_Treble_Square");
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            StartCoroutine(Scene0Dialogue());
        }
    }
}