using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObject;

    private bool inFight = false;
    private Enemy enemy = null;

    private int correct = 0;
    private int wrong = 0;

    private int kills = 0;

    private Animator animator;

    private void OnEnable() {
        animator = GetComponentInChildren<Animator>();

        GameManager.Instance.EventBus.Subscribe<EnemyTriggered>(EnemyTriggered);
        GameManager.Instance.EventBus.Subscribe<NotePressedEvent>(NotePressed);
        GameManager.Instance.EventBus.Subscribe<EnemyDead>(EnemyDied);
    }

    private void OnDisable() {
        GameManager.Instance.EventBus.Unsubscribe<EnemyTriggered>(EnemyTriggered); 
        GameManager.Instance.EventBus.Unsubscribe<NotePressedEvent>(NotePressed); 
        GameManager.Instance.EventBus.Unsubscribe<EnemyDead>(EnemyDied);
    }

    private void EnemyTriggered(EnemyTriggered eventData) {
        if (inFight == true) 
            return;
        
        inFight = true;
        enemy = eventData.Value;

        animator.SetBool("Fighting", true);

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;

        //GameManager.Instance.OpenStaff();
        //StartCoroutine(GameManager.Instance.SpawnAttackNotes());
    }

    private void NotePressed(NotePressedEvent eventData) {
        animator.SetTrigger("Attack");

        bool right = eventData.Value;

        if (right == true) {
            correct++;
           
        }
        
        if (right == false) {
            wrong++;

            if (GameManager.Instance.inTutorial == false) {
                GetComponent<Player>().Damage();
            }
        }
    }

    private void EnemyDied(EnemyDead eventdata) {
        StartCoroutine(CloseStaff());

        GameManager.Instance.totalKills++;
        kills++;
        textObject.text = "Kills: " + kills;
    }

    private IEnumerator CloseStaff() {
        
        yield return new WaitForSeconds(1);
        GameManager.Instance.CloseStaff();

        enemy = null;
        inFight = false;
        correct = 0;
        wrong = 0;

        animator.SetBool("Fighting", false);

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerJump>().enabled = true;
    }
}
