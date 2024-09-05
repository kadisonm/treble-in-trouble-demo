using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    
    private SpriteRenderer sprite;
    private Rigidbody2D body;
    private Animator animator;

    private void OnEnable()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        float directionX = Input.GetAxisRaw("Horizontal");

        if (directionX != 0)
        {
            animator.SetBool("Moving", true);
            sprite.flipX = directionX != 1;
        } else {
            animator.SetBool("Moving", false);
        }

        Vector2 velocity = body.velocity;

        velocity.x = directionX * Speed;

        body.velocity = velocity;
    }
}
