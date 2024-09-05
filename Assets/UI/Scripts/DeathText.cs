using System.Collections;
using TMPro;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    public TextMeshProUGUI textObject;

    private IEnumerator NextScene() {
        yield return new WaitForSeconds(10);
        GameManager.Instance.ChangeScene("01_Menu");
    }

    void Start()
    {
        StartCoroutine(NextScene());
        textObject.text = "You banished " + PlayerManager.Instance.TotalKills + " Abyssals though all your adventures.";
    }
}
