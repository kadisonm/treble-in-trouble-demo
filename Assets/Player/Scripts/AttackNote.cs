using System.Collections;
using UnityEngine;

public class AttackNote : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 offset;

    private Rigidbody2D body;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Loop(Vector3 direction) 
    {
        while (true) {
            yield return new WaitForFixedUpdate();

            body.velocity = direction * Time.deltaTime * Speed;
        }
    }

    public void MoveTo(Transform player, Enemy enemy) 
    {
        if (enemy == null) {
            return;
        }

        transform.position += offset;

        Vector3 enemyPosition = enemy.GetComponentInParent<Transform>().position;

        Vector3 direction = (enemyPosition - transform.position).normalized;

        StartCoroutine(Loop(direction));
    }
}
