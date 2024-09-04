using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    
    [SerializeField] private float freefallMultiplier;
    [SerializeField] private float jumpFallMultiplier;

    private Rigidbody2D body;
    private PlayerGrounded playerGrounded;
    private Animator animator;

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
        playerGrounded = GetComponent<PlayerGrounded>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        bool isGrounded = playerGrounded.IsGrounded();

        Vector2 velocity = body.velocity;

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = jumpPower;
            body.velocity = velocity;
        }

        //Set fall multipliers
        if (body.velocity.y < 0) {
            body.gravityScale = freefallMultiplier;
        }
        else {
            body.gravityScale = jumpFallMultiplier;
        }

        if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
    }
}
