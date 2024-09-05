using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            EventManager.Instance.EventBus.Publish<Events.EnemyTriggered>(new Events.EnemyTriggered {Value = GetComponentInParent<Enemy>()});
        }
    }
}
