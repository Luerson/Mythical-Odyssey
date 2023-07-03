using UnityEngine;

public class MainCharacter_OnHitState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;
    float timeCounter;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;

        timeCounter = 0;
        player.GetComponent<SpriteRenderer>().color = Color.yellow;

        if (player.transform.rotation.y == 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.left * 8;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.right * 8;
        }
    }


    public override void Update()
    {
        player.RecoverStamina();

        timeCounter += Time.deltaTime;
        if (timeCounter >= 0.35)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }
    }


    public override void Exit()
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
