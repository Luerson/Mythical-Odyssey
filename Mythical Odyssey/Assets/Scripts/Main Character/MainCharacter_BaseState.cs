using UnityEngine;

public abstract class MainCharacter_BaseState
{
    public abstract void Enter(MainCharacter_StateManager player);

    public abstract void Update(MainCharacter_StateManager player);

    public abstract void Exit(MainCharacter_StateManager player);
}
