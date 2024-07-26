using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float Speed;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();

        Player player = FindObjectOfType<Player>();

        Vector3 direction = (player.gameObject.transform.position - transform.position).normalized;

        StartCoroutine(Loop(direction));
    }

    private IEnumerator Loop(Vector3 direction) 
    {
        while (true) {
            yield return new WaitForFixedUpdate();

            body.velocity = direction * Time.deltaTime * Speed;
        }
    }
}
