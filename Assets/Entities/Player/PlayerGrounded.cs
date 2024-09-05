using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    private bool onGround;

    [SerializeField] private float DownCastLength = 0.95f;
    [SerializeField] private float RightCastLength = 0.95f;
    [SerializeField] private Vector3 DownCastOffset;
    [SerializeField] private Vector3 RightCastOffset;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform spriteTransform;

    private void Update()
    {
        bool grounded1 = Physics2D.Raycast(spriteTransform.position + DownCastOffset, Vector2.down, DownCastLength, groundLayer);
        bool grounded2 = Physics2D.Raycast(spriteTransform.position + RightCastOffset, Vector2.right, RightCastLength, groundLayer);
        onGround = grounded1 || grounded2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = onGround ? Color.green : Color.red;
        Gizmos.DrawLine(spriteTransform.position + DownCastOffset, spriteTransform.position + DownCastOffset + Vector3.down * DownCastLength);
        Gizmos.DrawLine(spriteTransform.position + RightCastOffset, spriteTransform.position + RightCastOffset + Vector3.right * RightCastLength);
    }

    public bool IsGrounded() { return onGround; }
}
