using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void Damage() 
    {
        health--;

        if (health <= 0) {
            Died();
        }
    }

    private void Died() {
        Destroy(gameObject);
    }
}
