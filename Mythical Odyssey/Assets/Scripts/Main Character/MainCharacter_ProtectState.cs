using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MainCharacter_ProtectState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;

        player.GetComponent<SpriteRenderer>().color = Color.blue;
    }


    public override void Update()
    {
        player.RecoverStamina();

        if (!Input.GetKey(KeyCode.P))
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }
    }


    public override void Exit()
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
