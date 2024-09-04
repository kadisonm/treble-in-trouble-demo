using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;

    public void SetText(string text)
    {
        textObject.text = text;
    }
}
