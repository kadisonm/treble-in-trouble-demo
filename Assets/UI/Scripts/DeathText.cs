using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathText : MonoBehaviour
{
    public TextMeshProUGUI textObject;

    private IEnumerator NextScene() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }

    void Start()
    {
        StartCoroutine(NextScene());
        textObject.text = "You banished " + GameManager.Instance.totalKills + " Abyssals though all your adventures.";
    }
}
