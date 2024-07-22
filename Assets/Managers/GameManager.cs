using UnityEngine;

using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int num = 1;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    public Staff staff;

    private void Awake() 
    { 

    }

    void Start() 
    {
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes() 
    {
        while (true)
        {
            if (num >= 16) {
                break;
            }
            
            staff.SpawnNote(num);
            num++;
            yield return new WaitForSeconds(.5f);
        }
    }
}