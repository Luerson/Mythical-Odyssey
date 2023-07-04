using UnityEngine;

public class MainCharacter_IdleState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;
    }


    public override void Update()
    {
        player.RecoverStamina();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.ChangeState(MainCharacter_StateManager.States.HEAL);
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(MainCharacter_StateManager.States.ATTACK);
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            player.ChangeState(MainCharacter_StateManager.States.DASH);
            return;
        }
    }


    public override void FixedUpdate()
    {
        if (player.Move())
        {
            player.ChangeState(MainCharacter_StateManager.States.RUN);
            return;
        }
    }



    public override void Exit()
    {

    }
}
