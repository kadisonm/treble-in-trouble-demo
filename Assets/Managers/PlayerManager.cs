using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("PlayerManager is NULL");

            return _instance;
        }
    }

    public int TotalKills;
    public int Kills;

    public bool Fighting = false;

    private GameObject Player;
    [SerializeField] private GameObject PlayerPrefab;

    private int health;

    private void Awake() 
    { 
        if (FindObjectsByType<PlayerManager>(FindObjectsSortMode.None).Length > 1)
            return;
        
        _instance = this;

        EventManager.Instance.EventBus.Subscribe<Events.EnemyDead>(EnemyDead);
    }

    public void SpawnPlayer()
    {
        GameObject spawn = GameObject.FindWithTag("PlayerSpawn");

        Vector3 spawnPosition = Vector3.zero;

        if (spawn) {
            spawnPosition = spawn.transform.position;
        }

        Player = Instantiate(PlayerPrefab, spawnPosition, Quaternion.identity);

        ToggleMovement(false);
        RestoreHealth(); 
    }

    public void ToggleMovement(bool toggle)
    {
        Player.GetComponent<PlayerMovement>().enabled = toggle;
        Player.GetComponent<PlayerJump>().enabled = toggle;
    }

    public void Damage() 
    {
        health--;

        EventManager.Instance.EventBus.Publish<Events.PlayerHurt>(new Events.PlayerHurt { Value = health });

        if (health <= 0) {
            Died();
        }
    }

    public void RestoreHealth() {
        health = 5;
        EventManager.Instance.EventBus.Publish<Events.PlayerHurt>(new Events.PlayerHurt { Value = health });
    }

    private void EnemyDead(Events.EnemyDead data)
    {
        TotalKills++;
    }

    private void Died() {
        ToggleMovement(false);
        GameManager.Instance.CloseStaff();
        GameManager.Instance.ChangeScene("01_Menu");
    }
}
