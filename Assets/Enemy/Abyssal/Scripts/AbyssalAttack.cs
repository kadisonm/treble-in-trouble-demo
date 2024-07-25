using System.Collections;
using UnityEditor;
using UnityEngine;

public class AbyssalAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private bool inFight = false;
    private bool touching = false;

    private IEnumerator Fight() {
        while (true) {
            yield return new WaitForSeconds(2);
            // attack
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") 
            return;

        touching = true;

        if (inFight == false) {
            GameManager.Instance.EventBus.Publish<EnemyTriggered>(new EnemyTriggered { Value = GetComponent<Enemy>() });
            StartCoroutine(Fight());
            GetComponent<AbyssalWalk>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag != "Player") 
            return;

        touching = false;
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector3 offset = new Vector3(collider.offset.x, collider.offset.y, 0);
        Gizmos.color = touching ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + offset, collider.size);
    }
}
