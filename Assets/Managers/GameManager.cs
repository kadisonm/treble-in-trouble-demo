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
            delay = 11;

            StartCoroutine(Scene1Dialogue());
        }

        if (current.name == "02_Outskirts") {
            StartCoroutine(Scene2Dialogue());
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

    private IEnumerator Scene1Dialogue()
    {
        DialogueManager.Instance.CreateNarration("Treble, once a city where melodies danced through the air, now echoes with the discord of monsters", 5);
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateNarration("You find yourself alone on the outskirts of town.", 3);
        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateNarration("Only you can sound the tunes of treble once more.", 3);
        yield return new WaitForSecondsRealtime(6);
        ChangeScene("02_Treble_Square");
    }

    private IEnumerator Scene2Dialogue()
    {
        yield return new WaitForSecondsRealtime(6);
        DialogueManager.Instance.CreateSpeech("Enemy", "~**Gbbuuurllllllllleee**~", 3);
    }
}