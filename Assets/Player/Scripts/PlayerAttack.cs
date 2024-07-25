using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool inFight = false;

    private int correct = 0;
    private int wrong = 0;

    private Animator animator;

    private void OnEnable() {
        animator = GetComponentInChildren<Animator>();

        GameManager.Instance.EventBus.Subscribe<EnemyTriggered>(EnemyTriggered);
        GameManager.Instance.EventBus.Subscribe<NotePressedEvent>(NotePressed);
    }

    private void OnDisable() {
       GameManager.Instance.EventBus.Unsubscribe<EnemyTriggered>(EnemyTriggered); 
       GameManager.Instance.EventBus.Unsubscribe<NotePressedEvent>(NotePressed); 
    }

    private void EnemyTriggered(EnemyTriggered eventData) {
        if (inFight == true) 
            return;
        
        inFight = true;

        animator.SetBool("Fighting", true);

        correct = 0;
        wrong = 0;

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;

        GameManager.Instance.OpenStaff();
        GameManager.Instance.SpawnAttackNotes();
    }

    private void NotePressed(NotePressedEvent eventData) {
        animator.SetTrigger("Attack");

        bool right = eventData.Value;

        if (right == true) {
            correct++;
        }
        
        if (right == false) {
            wrong++;
        }

        if (correct + wrong == 4) {
            // Attack ended
            StartCoroutine(CloseStaff());

        }
    }

    private IEnumerator CloseStaff() {
        yield return new WaitForSeconds(1);
        GameManager.Instance.CloseStaff();
    }

    private void AttackEnded() {

    }
}
