using UnityEngine;

public class MainCharacter_DashState : MainCharacter_BaseState
{
    MainCharacter_StateManager player;

    Vector3 initialPosition;


    public override void Enter(MainCharacter_StateManager player)
    {
        this.player = player;

        if (player.ConsumeStamina() == false)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }

        initialPosition = player.transform.position;
        player.GetComponent<Rigidbody2D>().velocity =
            Vector3.Normalize(Get_VelocityVector()) * (int)MainCharacter_StateManager.Speed.DASH_SPEED;
    }


    public override void Update()
    {
        if (Vector3.Distance(initialPosition, player.transform.position) >= 10)
        {
            player.ChangeState(MainCharacter_StateManager.States.IDLE);
            return;
        }
    }


    public override void Exit()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }


    /* Useful Methods */

    Vector3 Get_VelocityVector()
    {
        Vector3 mousePosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 velocityVector;
        velocityVector = mousePosition - player.transform.position;

        return velocityVector;
    }
}
