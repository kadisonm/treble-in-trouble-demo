using System.Collections.Generic;
using System.Collections;
using UnityEngine;

class Note {
    public GameObject gameObject;
    public int line;
}

public class Staff : MonoBehaviour
{
    [SerializeField] private GameObject NotePrefab;

    [SerializeField] private GameObject LinesObject;

    [SerializeField] private Sprite Normal;
    [SerializeField] private Sprite NormalLedger;
    [SerializeField] private Sprite Flipped;
    [SerializeField] private Sprite FlippedLedger;
    [SerializeField] private Sprite FlippedAboveLedger;
    [SerializeField] private Sprite FlippedDoubleLedger;
    [SerializeField] private Sprite Correct;
    [SerializeField] private Sprite Wrong;

    private List<Transform> lines = new();
    private List<Note> notes = new();

    private void OnEnable()
    {
        Transform[] transforms = LinesObject.GetComponentsInChildren<Transform>();

        foreach (Transform transform in transforms) {
            lines.Add(transform);
        }
    }

    private void OnDisable()
    {
        lines = new();
    }

    public void CreateNote(int note) {
        if (lines.Count < note) {
            return;
        }

        if (lines[note] == null) {
            return;
        }

        GameObject noteObject = Instantiate(NotePrefab, lines[note].position, Quaternion.identity, LinesObject.transform);

        Note newNote = new Note();
        newNote.gameObject = noteObject;
        newNote.line = note;

        Sprite sprite = Normal;

        if (note == 1) {
            sprite = NormalLedger;
        }

        if (note >= 7)  {
            sprite = Flipped;
        }

        if (note == 13) {
            sprite = FlippedLedger;
        }

        if (note == 14) {
            sprite = FlippedAboveLedger;
        }

        if (note == 15) {
            sprite = FlippedDoubleLedger;
        }

        SpriteRenderer renderer = noteObject.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        notes.Add(newNote);
    }

    public void CreateRandomNote(int min, int max) {
        int random = Random.Range(min, max + 1);
        CreateNote(random);
    }

    public IEnumerator SpawnInfiniteNotes(float interval, int min, int max) {
        while (true) 
        {
            CreateRandomNote(min, max);
            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator SpawnNotes(int amount, int min, int max, float interval) {
        int notesSpawned = 0;

        while (notesSpawned < amount) {
            notesSpawned++;
            CreateRandomNote(min, max);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator CorrectNote() {
        Note note = notes[0];

        SpriteRenderer sprite = note.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = Correct;

        notes.RemoveAt(0);

        EventManager.Instance.EventBus.Publish<Events.NotePressed>(new Events.NotePressed { Value = true });

        yield return new WaitForSeconds(1);

        Destroy(note.gameObject);
    }

    IEnumerator WrongNote() {
        Note note = notes[0];

        SpriteRenderer sprite = note.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = Wrong;

        notes.RemoveAt(0);

        EventManager.Instance.EventBus.Publish<Events.NotePressed>(new Events.NotePressed { Value = false });

        yield return new WaitForSeconds(1);
        
        Destroy(note.gameObject);
    }

    public void KeyPressed(string type)
    {
        if (notes.Count == 0) {
            return;
        }

        Note note = notes[0];

        bool isC = type == "C" && (note.line == 1 || note.line == 8 || note.line == 15);
        bool isD = type == "D" && (note.line == 2 || note.line == 9);
        bool isE = type == "E" && (note.line == 3 || note.line == 10);
        bool isF = type == "F" && (note.line == 4 || note.line == 11);
        bool isG = type == "G" && (note.line == 5 || note.line == 12);
        bool isA = type == "A" && (note.line == 6 || note.line == 13);
        bool isB = type == "B" && (note.line == 7 || note.line == 14);

        if (isC || isD || isE || isF || isG || isA || isB) {
            StartCoroutine(CorrectNote());
        } else {
            StartCoroutine(WrongNote());
        }
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.tag == "Note") {
            if (notes.Count > 0) {
                if (col.gameObject == notes[0].gameObject) {
                    StartCoroutine(WrongNote());
                }    
            }
        }
    }
}
