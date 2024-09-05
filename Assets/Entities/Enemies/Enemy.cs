using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 1;

    private Rigidbody2D body;
    private Animator animator;

    private bool canMove = true;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EventManager.Instance.EventBus.Subscribe<Events.EnemyHit>(Hit);
        EventManager.Instance.EventBus.Subscribe<Events.EnemyTriggered>(InFight);
        EventManager.Instance.EventBus.Subscribe<Events.NotePressed>(Attack);
    }

    private void OnDisable() {
        EventManager.Instance.EventBus.Unsubscribe<Events.EnemyHit>(Hit);
        EventManager.Instance.EventBus.Unsubscribe<Events.EnemyTriggered>(InFight);
        EventManager.Instance.EventBus.Unsubscribe<Events.NotePressed>(Attack);
    }

    private void Hit(Events.EnemyHit info) {
        if (info.Value == this) {
            Damage();
        }
    }

    private void InFight(Events.EnemyTriggered data)
    {
        if (data.Value == this)
        {
            animator.SetBool("Walking", false);
            canMove = false;
        }
    }

    private void Attack(Events.NotePressed data)
    {   
        if (data.Value == false) 
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Damage() 
    {
        health--;

        animator.SetTrigger("Hit");
        print("hit");

        if (health <= 0) {
            Died();
        }
    }

    private void Died() {
        EventManager.Instance.EventBus.Publish<Events.EnemyDead>(new Events.EnemyDead {Value = this});
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (canMove) {
            animator.SetBool("Walking", true);
            Vector2 velocity = body.velocity;
            velocity.x = -1 * speed;
            body.velocity = velocity;    
        }
    }
}
