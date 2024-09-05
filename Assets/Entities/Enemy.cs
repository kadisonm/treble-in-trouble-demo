using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private void OnEnable() {
        EventManager.Instance.EventBus.Subscribe<Events.EnemyHit>(Hit);
    }

    private void OnDisable() {
        EventManager.Instance.EventBus.Unsubscribe<Events.EnemyHit>(Hit);
    }

    private void Hit(Events.EnemyHit info) {
        if (info.Value == this) {
            Damage();
        }
    }

    public void Damage() 
    {
        health--;

        if (health <= 0) {
            Died();
        }
    }

    private void Died() {
        EventManager.Instance.EventBus.Publish<Events.EnemyDead>(new Events.EnemyDead {Value = this});
        Destroy(gameObject);
    }
}
