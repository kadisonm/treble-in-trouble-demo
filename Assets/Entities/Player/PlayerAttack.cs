using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool inFight = false;
    private Enemy enemy = null;

    private Animator animator;

    private void OnEnable() {
        animator = GetComponent<Animator>();

        EventManager.Instance.EventBus.Subscribe<Events.EnemyTriggered>(EnemyTriggered);
        EventManager.Instance.EventBus.Subscribe<Events.NotePressed>(NotePressed);
        EventManager.Instance.EventBus.Subscribe<Events.EnemyDead>(EnemyDied);
    }

    private void OnDisable() {
        EventManager.Instance.EventBus.Unsubscribe<Events.EnemyTriggered>(EnemyTriggered); 
        EventManager.Instance.EventBus.Unsubscribe<Events.NotePressed>(NotePressed); 
        EventManager.Instance.EventBus.Unsubscribe<Events.EnemyDead>(EnemyDied);
    }

    private void EnemyTriggered(Events.EnemyTriggered eventData) {
        if (inFight == true) 
            return;
        
        inFight = true;
        enemy = eventData.Value;

        animator.SetBool("Fighting", true);

        GameManager.Instance.OpenStaff();
        GameManager.Instance.SpawnAttackNotes();

        PlayerManager.Instance.Fighting = true;
    }

    private void NotePressed(Events.NotePressed eventData) {
        bool right = eventData.Value;

        if (right) {
            if (enemy) {
                print("attack");
                animator.SetTrigger("Attack");
                enemy.GetComponent<Enemy>().Damage();
            }
        }
        else 
        {
            if (GameManager.Instance.inTutorial == false) {
                PlayerManager.Instance.Damage();
            }
        }
    }

    private void EnemyDied(Events.EnemyDead eventdata) {
        PlayerManager.Instance.Fighting = false;

        StartCoroutine(CloseStaff());
    }

    private IEnumerator CloseStaff() {
        
        yield return new WaitForSeconds(1);
        GameManager.Instance.CloseStaff();

        enemy = null;
        inFight = false;

        animator.SetBool("Fighting", false);
    }
}
