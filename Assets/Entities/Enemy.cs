using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private void OnEnable() {
        GameManager.Instance.EventBus.Subscribe<EnemyHit>(Hit);
    }

    private void OnDisable() {
        GameManager.Instance.EventBus.Unsubscribe<EnemyHit>(Hit);
    }

    private void Hit(EnemyHit info) {
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
        GameManager.Instance.EventBus.Publish<EnemyDead>(new EnemyDead {Value = this});
        Destroy(gameObject);
    }
}
