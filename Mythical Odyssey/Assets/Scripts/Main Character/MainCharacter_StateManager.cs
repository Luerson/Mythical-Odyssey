using System;
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
        ATTACK,
        HEAL,
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

    // Constants
    public const int INITIAL_HEALTH = 100;
    public const float INITIAL_STAMINA = 100f;
    public const int INITIAL_DAMAGE = 10;
    
    public const float NORMAL_SPEED = 120f;
    public const float DASH_SPEED = 3 * NORMAL_SPEED;
    public const float HEALING_SPEED = NORMAL_SPEED / 2;
    public const float STAMINA_RECOVER_SPEED = INITIAL_STAMINA / 10;

    public const float DASH_STAMINA_CONSUME = INITIAL_STAMINA / 2;


    // Variables
    int maxHealth;
    int currentHealth;

    float maxStamina;
    float currentStamina;

    float currentSpeed;

    int currentDamage;



    /**********************/
    /* Main Unity Methods */
    /**********************/


    void Start()
    {
        maxHealth = INITIAL_HEALTH;
        currentHealth = INITIAL_HEALTH;

        maxStamina = INITIAL_STAMINA;
        currentStamina = INITIAL_STAMINA;

        currentDamage = INITIAL_DAMAGE;

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


    public void RecoverStamina()
    {
        currentStamina = Mathf.Max(maxStamina, currentStamina + (STAMINA_RECOVER_SPEED * Time.deltaTime));
    }


    public void TakeDamage(int amount)
    {
        currentHealth = Math.Max(0, currentHealth - amount);

        if (currentHealth == 0)
            ChangeState(States.DEAD);
        else
            ChangeState(States.ON_HIT);
    }


    public void Heal(int amount)
    {
        currentHealth = Math.Min(maxHealth, currentHealth + amount);
    }
}
