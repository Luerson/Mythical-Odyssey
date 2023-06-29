using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Health related constants
    public enum Health
    {
        INITIAL_HEALTH = 100,

        INITIAL_HEAL_AMOUNT = INITIAL_HEALTH / 4
    }

    // Stamina related constants
    public enum Stamina
    {
        INITIAL_STAMINA = 100,

        RECOVER_SPEED = INITIAL_STAMINA / 10,
        DASH_STAMINA_CONSUME = INITIAL_STAMINA / 2
    }

    // Values the character speed may assume
    public enum Speed
    {
        NORMAL_SPEED = 120,
        
        DASH_SPEED = 3 * NORMAL_SPEED,
        HEALING_SPEED = NORMAL_SPEED / 2
    }

    // Damage related constants
    public enum Damage
    {
        INITIAL_DAMAGE = 10
    }


    /* End of Enums declarations */


    // Declaration and Initialization of each states classes
    MainCharacter_BaseState currentState;

    MainCharacter_BaseState IdleState = new MainCharacter_IdleState();
    MainCharacter_BaseState RunState = new MainCharacter_RunState();
    MainCharacter_BaseState DashState = new MainCharacter_DashState();
    MainCharacter_BaseState AttackState = new MainCharacter_AttackState();
    MainCharacter_BaseState HealState = new MainCharacter_HealState();
    MainCharacter_BaseState ProtectState = new MainCharacter_ProtectState();
    MainCharacter_BaseState OnHitState = new MainCharacter_OnHitState();
    MainCharacter_BaseState DeadState = new MainCharacter_DeadState();



    // Variables
    int maxHealth;
    int currentHealth;
    int currentHealAmount;

    float maxStamina;
    float currentStamina;

    float currentSpeed;

    int currentDamage;



    /**********************/
    /* Main Unity Methods */
    /**********************/


    void Start()
    {
        maxHealth = (int)Health.INITIAL_HEALTH;
        currentHealth = (int)Health.INITIAL_HEALTH;
        currentHealAmount = (int)Health.INITIAL_HEAL_AMOUNT;

        maxStamina = (float)Stamina.INITIAL_STAMINA;
        currentStamina = (float)Stamina.INITIAL_STAMINA; 

        currentDamage = (int)Damage.INITIAL_DAMAGE;

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
            case States.ATTACK: currentState = AttackState; break;
            case States.HEAL: currentState = HealState; break;
            case States.PROTECT: currentState = ProtectState; break;
            case States.ON_HIT: currentState = OnHitState; break;
            case States.DEAD: currentState = DeadState; break;
            default: Debug.Log("State not included in ChangeState method!"); break;
        }
        currentState.Enter(this);
    }


    public void RecoverStamina()
    {
        currentStamina = Mathf.Max(maxStamina, currentStamina + ((int)Stamina.RECOVER_SPEED * Time.deltaTime));
    }


    public void TakeDamage(int amount)
    {
        currentHealth = Math.Max(0, currentHealth - amount);

        if (currentHealth == 0)
            ChangeState(States.DEAD);
        else
            ChangeState(States.ON_HIT);
    }


    public void Heal()
    {
        currentHealth = Math.Min(maxHealth, currentHealth + currentHealAmount);
    }

    
    public void ChangeSpeed(Speed speed)
    {
        currentSpeed = (int)speed;
    }


    /* The next methods must be used to move the character. */
    /* The'll return true if the character moves            */
    public bool MoveUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // TODO: move the character
            return true;
        }
        return false;
    }


    public bool MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // TODO: move the character
            return true;
        }
        return false;
    }


    public bool MoveDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            // TODO: move the character
            return true;
        }
        return false;
    }


    public bool MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // TODO: move the character
            return true;
        }
        return false;
    }
}
