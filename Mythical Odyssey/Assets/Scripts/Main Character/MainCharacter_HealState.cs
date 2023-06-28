using UnityEngine;

public class MainCharacter_HealState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;
    }


    public override void Update()
    {

    }


    public override void Exit()
    {

    }
}
