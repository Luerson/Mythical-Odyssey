using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_StateManager : MonoBehaviour
{
    // All Main Character States.
    // The States must be public so it can be used by the States Classes to change
    // the main character current state
    public enum States
    {
        IDLE,
        RUN,
        DASH,
        PROTECT,
        ON_HIT,
        DEAD
    }


    // Declaration and Initialization of each states classes
    MainCharacter_BaseState currentState;

    MainCharacter_BaseState IdleState = new MainCharacter_IdleState();
    MainCharacter_BaseState RunState = new MainCharacter_RunState();
    MainCharacter_BaseState DashState = new MainCharacter_DashState();
    MainCharacter_BaseState ProtectState = new MainCharacter_ProtectState();
    MainCharacter_BaseState OnHitState = new MainCharacter_OnHitState();
    MainCharacter_BaseState DeadState = new MainCharacter_DeadState();



    /**********************/
    /* Main Unity Methods */
    /**********************/


    void Start()
    {
        // Player will start idle
        ChangeState(States.IDLE);
    }


    void Update()
    {
        // Update player (regardless the current state, the Update method will handle it by its own)
        currentState.Update();
    }


    ////////////////////////////////////////////
    /* From now on there will be only methods */
    ////////////////////////////////////////////


    /*
     * Simple function to change the current state
     */
    public void ChangeState(States state)
    {
        // Get out of current state
        if (currentState != null)
            currentState.Exit();

        // Entering new state
        switch(state)
        {
            case States.IDLE: currentState = IdleState; break;
            case States.RUN: currentState = RunState; break;
            case States.DASH: currentState = DashState; break;
            case States.PROTECT: currentState = ProtectState; break;
            case States.ON_HIT: currentState = OnHitState; break;
            case States.DEAD: currentState = DeadState; break;
        }
        currentState.Enter(this);
    }
}
