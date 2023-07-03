using UnityEngine;

public class MainCharacter_HealState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;
    float timeCounter;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;
        timeCounter = 0;

        if (player.Get_HealingPotionsCounter() == 0)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }

        player.GetComponent<SpriteRenderer>().color = Color.green;
        player.SetSpeed(MainCharacter_StateManager.Speed.HEALING_SPEED);
    }


    public override void Update()
    {
        player.RecoverStamina();

        timeCounter += Time.deltaTime;
        player.MoveUp();
        player.MoveDown();
        player.MoveLeft();
        player.MoveRight();


        if (timeCounter >= (int)MainCharacter_StateManager.HealingPotion.TIME_TO_COMPLETE_HEALING)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
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


    public override void Exit()
    {
        if (timeCounter >= (int)MainCharacter_StateManager.HealingPotion.TIME_TO_COMPLETE_HEALING)
            player.Heal();

        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
