using UnityEngine;

class PlayerVelocity : MonoBehaviour
{
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float defaultFallMultiplier;

    private Rigidbody2D body;

    private float movementForce = 0;
    private float jumpForce = 0;

    private void Awake()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameManager.Instance.EventBus.Subscribe<MoveVelocityEvent>(SetMovement);
        GameManager.Instance.EventBus.Subscribe<JumpVelocityEvent>(SetJump);
    }

    private void OnDisable()
    {
        GameManager.Instance.EventBus.Unsubscribe<MoveVelocityEvent>(SetMovement);
        GameManager.Instance.EventBus.Unsubscribe<JumpVelocityEvent>(SetJump);
    }

    private void SetMovement(MoveVelocityEvent eventData)
    {
        movementForce = eventData.Value;
    }

    private void SetJump(JumpVelocityEvent eventData)
    {
        jumpForce = eventData.Value;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = body.velocity;

        //Set horizontal velocities
        velocity.x = movementForce;
            
        //Set vertical velocities
        if (jumpForce != 0)
        {
            velocity.y = jumpForce;
            jumpForce = 0;
        }

        body.velocity = velocity;

        //Set fall multipliers
        if (body.velocity.y < 0)
            body.gravityScale = fallMultiplier;
        else
            body.gravityScale = defaultFallMultiplier;
    }
}
