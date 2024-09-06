using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;

    public static DialogueManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("DialogueManager is NULL");

            return _instance;
        }
    }

    public GameObject NarrationBox;
    public GameObject HalfNarrationBox;
    public GameObject SpeechBox;

    private void Awake() 
    { 
        if (FindObjectsByType<DialogueManager>(FindObjectsSortMode.None).Length > 1)
            return;

        _instance = this; 
    } 

    public void CreateNarration(string text, int duration = 2)
    {
        GameObject canvas = GameObject.FindWithTag("Canvas");

        if (canvas) {
            GameObject box = Instantiate(NarrationBox, canvas.transform);
            NarrationBox narration = box.GetComponent<NarrationBox>();
            narration.SetText(text);
            Destroy(box, duration);    
        }
    }

    public void CreateHalfNarration(string text, int duration = 2)
    {
        GameObject canvas = GameObject.FindWithTag("Canvas");

        if (canvas) {
            GameObject box = Instantiate(HalfNarrationBox, canvas.transform);
            NarrationBox narration = box.GetComponent<NarrationBox>();
            narration.SetText(text);
            Destroy(box, duration);    
        }
        
    }

    public void CreateSpeech(string name, string text, int duration = 2)
    {
        GameObject canvas = GameObject.FindWithTag("Canvas");

        if (canvas) {
            GameObject box = Instantiate(SpeechBox, canvas.transform);
            SpeechBox speech = box.GetComponent<SpeechBox>();
            speech.SetText(name, text);
            Destroy(box, duration);
        }
    }
}
