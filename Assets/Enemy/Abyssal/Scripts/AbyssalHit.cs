using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Note") {
            GameManager.Instance.EventBus.Publish<EnemyHit>(new EnemyHit {Value = GetComponentInParent<Enemy>()});
            Destroy(col.gameObject);
        }
    }
}
