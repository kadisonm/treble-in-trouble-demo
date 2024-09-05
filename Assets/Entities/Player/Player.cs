using UnityEngine;
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

        EventManager.Instance.EventBus.Publish<Events.PlayerHurt>(new Events.PlayerHurt { Value = health });

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
