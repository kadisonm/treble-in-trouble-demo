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
    public GameObject SpeechBox;

    [SerializeField] private Transform canvas;

    private void Awake() 
    { 
        _instance = this; 
    } 

    public void CreateNarration(string text, int duration = 2)
    {
        GameObject box = Instantiate(NarrationBox, canvas);
        NarrationBox narration = box.GetComponent<NarrationBox>();
        narration.SetText(text);
        Destroy(box, duration);
    }

    public void CreateSpeech(string name, string text, int duration = 2)
    {
        GameObject box = Instantiate(SpeechBox, canvas);
        SpeechBox speech = box.GetComponent<SpeechBox>();
        speech.SetText(name, text);
        Destroy(box, duration);
    }
}
