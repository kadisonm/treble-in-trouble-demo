using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public void Damage() 
    {
        health--;

        if (health <= 0) {
            Died();
        }
    }

    private void Died() {
        SceneManager.LoadScene("Died");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Projectile") {
            Destroy(col.gameObject);
            Damage();
        }
    }
}
