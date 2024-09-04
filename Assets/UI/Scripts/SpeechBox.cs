using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameObject;
    [SerializeField] private TextMeshProUGUI textObject;

    public void SetText(string name, string text)
    {
        nameObject.text = name;
        textObject.text = text;
    }
}
