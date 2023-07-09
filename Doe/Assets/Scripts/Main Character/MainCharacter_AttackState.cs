using Unity.VisualScripting;
using UnityEngine;

public class MainCharacter_AttackState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;
    Animator Animator;
    float currentTime;

    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;
        Animator = player.gameObject.GetComponent<Animator>();
        currentTime = 0;

        Animator.SetBool("Attacking", true);
        player.attackPoint.position = Get_AttackVector();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, 0.7f);
        foreach(Collider2D hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyBehavior>().TakeDamage(20);
            }
        }
    }


    public override void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 0.4)
        {
            Animator.SetBool("Attacking", false);
        }

        if (currentTime >= 0.75f)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
        }
    }


    public override void FixedUpdate()
    {

    }


    public override void Exit()
    {
        
    }


    /* Useful methods */


    Vector3 Get_AttackVector()
    {
        Vector3 mousePosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 attackVector;
        attackVector = mousePosition - player.transform.position;

        return Vector3.Normalize(attackVector) + player.transform.position;
    }
}
