using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    private TextMeshProUGUI textObject;

    private void OnEnable() 
    {
        textObject = GetComponent<TextMeshProUGUI>();
        EventManager.Instance.EventBus.Subscribe<Events.PlayerHurt>(UpdateText);
    }

    private void OnDisable()
    {
        EventManager.Instance.EventBus.Unsubscribe<Events.PlayerHurt>(UpdateText);
    }

    private void UpdateText(Events.PlayerHurt data)
    {
        print("Update");
        textObject.text = "Health: " + data.Value.ToString();
    }
}
