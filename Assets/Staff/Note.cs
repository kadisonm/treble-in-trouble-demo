using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Time.deltaTime * -speed, 0, 0);
    }
}
