using UnityEngine;

public abstract class MainCharacter_BaseState
{
    MainCharacter_StateManager player;

    public abstract void Enter(MainCharacter_StateManager player);

    public abstract void Update();

    public abstract void Exit();
}
