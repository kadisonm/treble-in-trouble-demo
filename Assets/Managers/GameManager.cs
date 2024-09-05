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

    public EventBus<IEvent> EventBus { get; set; }

    public bool inTutorial = false;
    public int totalKills = 0;

    private GameObject staff;
    [SerializeField] private GameObject staffPrefab;

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
            staff.GetComponent<Staff>().CreateRandomNote(1, 15);
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator SpawnInfiniteNotes() 
    {
        if (staff == null)
            yield break;

        Staff staffScript = staff.GetComponent<Staff>();

        while (true)
        {
            staffScript.CreateRandomNote(1, 15);
            yield return new WaitForSeconds(2);
        }
    }

    private void Awake() 
    { 
        _instance = this; 
        EventBus = new EventBus<IEvent>();
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
            StartCoroutine(Scene2Dialogue());
        }

        if (transition)
        {
            StartCoroutine(transition.FadeIn(1.5f, delay));
        }
    }

    private IEnumerator Scene1Dialogue()
    {
        DialogueManager.Instance.CreateNarration("You find yourself alone on the outskirts of the once city Treble.", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateNarration("It was once a city where melodies danced through the air", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateNarration("but now it echoes with the discord of monsters.", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateNarration("Only you can sound the tunes of treble once more.", 4);
        yield return new WaitForSecondsRealtime(7);
        ChangeScene("02_Outskirts");
    }

    private IEnumerator Scene2Dialogue()
    {
        DialogueManager.Instance.CreateSpeech("You", "Where am I...", 3);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateSpeech("You", "Is this a piano?", 3);
        yield return new WaitForSecondsRealtime(3);

        inTutorial = true;
        OpenStaff();

        Staff staffController = staff.GetComponent<Staff>();
        
        DialogueManager.Instance.CreateHalfNarration("This is your piano, it is a powerful magical weapon.", 4);
        yield return new WaitForSecondsRealtime(4);
        DialogueManager.Instance.CreateHalfNarration("Above you is a musical staff. When enemies attack, notes will spawn along it like so.", 6);
        yield return new WaitForSecondsRealtime(3);

        StartCoroutine(staffController.SpawnNotes(4, 1, 15, 1f));

        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateHalfNarration("You must press the correct key on the piano before the red line.", 5);
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Failure to do so will result in injury.", 4);
        yield return new WaitForSecondsRealtime(4);

        DialogueManager.Instance.CreateHalfNarration("First you must learn the 'C' note. This is the first note on your piano.", 6);
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
        yield return new WaitForSecondsRealtime(3);

        DialogueManager.Instance.CreateHalfNarration("Lastly lets try both at the same time.", 4);
        yield return new WaitForSecondsRealtime(4);
        StartCoroutine(staffController.SpawnNotes(4, 1, 2, 1f));
        yield return new WaitForSecondsRealtime(5);
        DialogueManager.Instance.CreateHalfNarration("Goodjob! You're prepared for anything now.", 3);

        inTutorial = false;
        CloseStaff();

        yield return new WaitForSecondsRealtime(5);

        DialogueManager.Instance.CreateSpeech("Enemy", "~~Grrrrrrrbbble~~", 3);
        yield return new WaitForSecondsRealtime(3);
        DialogueManager.Instance.CreateSpeech("You", "What was that sound?", 3);
        yield return new WaitForSecondsRealtime(3);
    }
}