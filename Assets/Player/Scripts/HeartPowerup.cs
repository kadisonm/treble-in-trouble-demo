using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            col.GetComponent<Player>().RestoreHealth();
            Destroy(gameObject);    
        }
    }
}
