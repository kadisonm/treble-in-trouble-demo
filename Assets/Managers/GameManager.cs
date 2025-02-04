using UnityEngine;

using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int totalKills = 0;  

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    public EventBus<IEvent> EventBus { get; set; }

    [SerializeField] private GameObject staffPrefab;
    private GameObject staff;

    private void Awake() 
    { 
        _instance = this; 
        EventBus = new EventBus<IEvent>();
        SceneManager.activeSceneChanged += NewScene;
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= NewScene;
    }

    private void NewScene(Scene current, Scene next) 
    {
		if (next.name == "InfiniteDemo") {
            OpenStaff();
            StartCoroutine(SpawnInfiniteNotes());
        } else if (next.name == "Demo") {
            
        }
    }

    public void OpenStaff() {
        Transform ui = Camera.main.transform.GetChild(0);
        staff = Instantiate(staffPrefab, ui);
    }

    public void CloseStaff() {
        Destroy(staff);
        staff = null;
    }

    public IEnumerator SpawnAttackNotes() {
        if (staff == null) {
            yield break;
        }

        int notesSpawned = 0;

        while (notesSpawned < 4) {
            if (staff == null) {
                yield break;
            }

            notesSpawned++;
            staff.GetComponent<Staff>().CreateRandomNote();
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator SpawnInfiniteNotes() 
    {
        if (staff == null)
            yield break;

        while (true)
        {
            staff.GetComponent<Staff>().CreateRandomNote();
            yield return new WaitForSeconds(2);
        }
    }
}