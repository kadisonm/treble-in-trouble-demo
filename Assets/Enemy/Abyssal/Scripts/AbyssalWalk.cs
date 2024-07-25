using System.Collections;
using UnityEngine;

public class AbyssalWalk : MonoBehaviour
{
    [SerializeField] private float Speed;
    
    private Rigidbody2D body;
    private SpriteRenderer sprite;

    int direction = 0;

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

        StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection() {
        while (true) {
            direction = 1;
            yield return new WaitForSeconds(Random.Range(1, 3));
            direction = 0;
            yield return new WaitForSeconds(Random.Range(1, 3));
            direction = -1;
            yield return new WaitForSeconds(Random.Range(1, 3));
            direction = 0;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    private void FixedUpdate()
    {
        if (direction != 0)
        {
            sprite.flipX = direction == 1;
        }

        Vector2 velocity = body.velocity;
        velocity.x = direction * Speed;
        body.velocity = velocity;
    }
}
