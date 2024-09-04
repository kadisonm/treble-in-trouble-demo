using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private int health = 5;

    public void OnEnable() {
        textObject.text = "Health: " + health;
    }

    public void RestoreHealth() {
        health = 5;
        textObject.text = "Health: " + health;
    }

    public void Damage() 
    {
        health--;
        textObject.text = "Health: " + health;

        if (health <= 0) {
            Died();
        }
    }

    private void Died() {
        GameManager.Instance.ChangeScene("03_Dead");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Projectile") {
            Destroy(col.gameObject);
            Damage();
        }
    }
}
