using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public GameObject NoteObject;

    public Sprite Normal;
    public Sprite NormalLedger;
    public Sprite Flipped;
    public Sprite FlippedLedger;
    public Sprite FlippedAboveLedger;
    public Sprite FlippedDoubleLedger;

    private readonly List<Vector3> lines = new();

    void Start()
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform transform in transforms) {
            lines.Add(transform.position);
        }
    }

    public void SpawnNote(int note) {
        if (lines.Count < note) {
            return;
        }

        GameObject newNote = Instantiate(NoteObject, lines[note], Quaternion.identity);

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

        SpriteRenderer renderer = newNote.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
 
        Debug.Log(renderer.sprite);
    }

    public void SpawnRandomNote() {
        int line = Random.Range(1, 16);

        SpawnNote(line);
    }
}
