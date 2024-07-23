using UnityEngine;

using System.Collections;

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
            staff.SpawnRandomNote();
            yield return new WaitForSeconds(2);
        }
    }
}