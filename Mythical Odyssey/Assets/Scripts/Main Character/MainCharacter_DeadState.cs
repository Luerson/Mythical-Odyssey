using UnityEngine;

public class MainCharacter_DeadState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;

        player.GetComponent<SpriteRenderer>().color = Color.red;
    }


    public override void Update()
    {

    }


    public override void Exit()
    {

    }
}
