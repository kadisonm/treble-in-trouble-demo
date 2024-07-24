using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    
    [SerializeField] private float jumpPower;
    
    [SerializeField] private float freefallMultiplier;
    [SerializeField] private float jumpFallMultiplier;

    private Rigidbody2D body;
    private PlayerGrounded playerGrounded;
    private Animator animator;

    private bool previousState;

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
        playerGrounded = GetComponent<PlayerGrounded>();
        animator = GetComponentInChildren<Animator>();
    }

    private void ChangeFallAnimation(bool state)
    {
        if (previousState != state)
        {
            previousState = state;
        }
    }

    private void Update()
    {
        bool isGrounded = playerGrounded.IsGrounded();

        Vector2 velocity = body.velocity;

        if (Input.GetButton("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
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
            animator.SetBool("Grounded", true);
            ChangeFallAnimation(false);
        }
        else
        {
            animator.SetBool("Grounded", false);
            ChangeFallAnimation(true);
        }
    }

    
}
