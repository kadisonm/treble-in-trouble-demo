using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool inTutorial = false;
    public int totalKills = 0;

    private GameObject staff;
    [SerializeField] private GameObject staffPrefab;
    [SerializeField] private GameObject skeletonPrefab;

    private int minNote = 1;
    private int maxNote = 1;

    public void ChangeScene(string name)
    {
        SceneTransition transition = FindFirstObjectByType<SceneTransition>();

        if (transition)
        {
            StartCoroutine(transition.FadeOut(1.5f, 0, () => {
                SceneManager.LoadScene(name);
            }));
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

    public void SpawnAttackNotes() {
        if (PlayerManager.Instance.Fighting == false) {
            Staff staffController = staff.GetComponent<Staff>();
            StartCoroutine(staffController.SpawnInfiniteNotes(1.5f, minNote, maxNote));    
        }
    }

    private void Awake() 
    { 
        if (FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length > 1)
            return;

        _instance = this; 
        SceneManager.activeSceneChanged += NewScene;    
    } 

    private void NewScene(Scene old, Scene current)
    {
        SceneTransition transition = FindFirstObjectByType<SceneTransition>();

        float delay = 0f;

        if (current.name == "01_Menu") {
            delay = 16;

            StartCoroutine(Scene1Dialogue());
        }

        if (current.name == "02_Outskirts") {
            PlayerManager.Instance.SpawnPlayer();
            StartCoroutine(Scene2Dialogue());
        }

        if (transition)
        {
            StartCoroutine(transition.FadeIn(1.5f, delay));
        }
    }

    private void SpawnEnemy() {
        GameObject spawn = GameObject.FindWithTag("EnemySpawn");
        Vector3 spawnPosition = Vector3.zero;

        if (spawn) {
            spawnPosition = spawn.transform.position;
        }

        GameObject enemy = Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator Scene1Dialogue()
    {
        DialogueManager.Instance.CreateNarration("You find yourself alone on the outskirts of the once city Treble.", 6);
        yield return new WaitForSecondsRealtime(6);
        DialogueManager.Instance.CreateNarration("It was once a city where melodies danced through the air.", 5);
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateNarration("but now it echoes with the discord of monsters.", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateNarration("Only you can sound the tunes of treble once more.", 4);
        yield return new WaitForSecondsRealtime(7);
        ChangeScene("02_Outskirts");
    }

    private IEnumerator Wave1()
    {
        DialogueManager.Instance.CreateSpeech("Unknown", "~~Krrrr Krrr~~", 3);
        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateSpeech("You", "What was that sound?", 3);
        yield return new WaitForSecondsRealtime(3);

        int totalEnemies = 0;

        void Wave1_onEnemyDead(Events.EnemyDead data) 
        {
            totalEnemies++;
            
            if (totalEnemies >= 2) {
                EventManager.Instance.EventBus.Unsubscribe<Events.EnemyDead>(Wave1_onEnemyDead);
                StartCoroutine(Wave2());    
            } else {
                SpawnEnemy();
            }
        };

        EventManager.Instance.EventBus.Subscribe<Events.EnemyDead>(Wave1_onEnemyDead);

        SpawnEnemy();

        DialogueManager.Instance.CreateNarration("Don't miss a note or it will attack!", 3);
    }

    private IEnumerator Wave2()
    {
        yield return new WaitForSecondsRealtime(2);
        DialogueManager.Instance.CreateSpeech("You", "Wow, that was close.", 3);
        yield return new WaitForSecondsRealtime(3);
        PlayerManager.Instance.RestoreHealth();
        DialogueManager.Instance.CreateNarration("Your health has been restored.", 3);
        yield return new WaitForSecondsRealtime(3);

        OpenStaff();

        Staff staffController = staff.GetComponent<Staff>();
        inTutorial = true;

        DialogueManager.Instance.CreateHalfNarration("It is time to learn your third note.", 3);
        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateHalfNarration("You will learn the 'E' note. This is the third note on your piano.", 3);
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(staffController.SpawnNotes(4, 3, 3, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Nice! You just learnt the 'E' note!", 3);
        maxNote = 3;
        yield return new WaitForSecondsRealtime(3);

        DialogueManager.Instance.CreateHalfNarration("Let's try all the notes you've learnt at the same time.", 4);
        yield return new WaitForSecondsRealtime(4);
        StartCoroutine(staffController.SpawnNotes(4, 1, 3, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Goodjob!", 3);
        yield return new WaitForSecondsRealtime(3);

        inTutorial = false;
        CloseStaff();
        
        int wave2_enemies = 0;

        void Wave2_onEnemyDead(Events.EnemyDead data) 
        {
            wave2_enemies++;

            if (wave2_enemies >= 3) {
                EventManager.Instance.EventBus.Unsubscribe<Events.EnemyDead>(Wave2_onEnemyDead);

                DialogueManager.Instance.CreateSpeech("You", "Phew, I think I'm getting the hang of this.", 4);
            } else {
                SpawnEnemy();
            }
        };

        EventManager.Instance.EventBus.Subscribe<Events.EnemyDead>(Wave2_onEnemyDead);

        SpawnEnemy();
    }

    private IEnumerator Scene2Dialogue()
    {
        DialogueManager.Instance.CreateSpeech("You", "Where am I...", 5);
        yield return new WaitForSecondsRealtime(7);
        DialogueManager.Instance.CreateSpeech("You", "Is this a piano?", 3);
        yield return new WaitForSecondsRealtime(3);

        OpenStaff();

        Staff staffController = staff.GetComponent<Staff>();
        
        DialogueManager.Instance.CreateHalfNarration("This is your piano scythe, it is a powerful magical weapon.", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateHalfNarration("Above you is a musical staff. When enemies attack, notes will spawn along it like so.", 6);
        yield return new WaitForSecondsRealtime(3);

        StartCoroutine(staffController.SpawnNotes(4, 1, 15, 1f));

        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateHalfNarration("You must press the correct key on the piano before the red line.", 5);
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Failure to do so will result in injury.", 4);
        yield return new WaitForSecondsRealtime(4);
        PlayerManager.Instance.RestoreHealth();
        DialogueManager.Instance.CreateHalfNarration("Your health has been restored.", 3);
        yield return new WaitForSecondsRealtime(3);

        inTutorial = true;

        DialogueManager.Instance.CreateHalfNarration("Firstly, you must learn the 'C' note. This is the first note on your piano.", 6);
        yield return new WaitForSecondsRealtime(6);
        DialogueManager.Instance.CreateHalfNarration("Give it a try now.", 3);
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(staffController.SpawnNotes(4, 1, 1, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Nice! You just learnt the 'C' note!", 3);
        yield return new WaitForSecondsRealtime(3);

        DialogueManager.Instance.CreateHalfNarration("Now you will learn the 'D' note. This is the second note on your piano.", 3);
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(staffController.SpawnNotes(4, 2, 2, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Nice! You just learnt the 'D' note!", 3);
        maxNote = 2;
        yield return new WaitForSecondsRealtime(3);

        DialogueManager.Instance.CreateHalfNarration("Lastly, lets try both at the same time.", 4);
        yield return new WaitForSecondsRealtime(4);
        StartCoroutine(staffController.SpawnNotes(4, 1, 2, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Goodjob! You're prepared for anything now.", 3);

        CloseStaff();
        inTutorial = false;

        yield return new WaitForSecondsRealtime(5);

        // Wave 1

        StartCoroutine(Wave1());
    }
}