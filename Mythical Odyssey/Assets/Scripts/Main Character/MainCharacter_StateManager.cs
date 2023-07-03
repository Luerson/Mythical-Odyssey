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
        // PROTECT,
        ON_HIT,
        DEAD
    }

    // Health related constants
    public enum Health
    {
        INITIAL_HEALTH = 100
    }

    // Healing potion related constants
    public enum HealingPotion
    {
        INITIAL_HEAL_AMOUNT = Health.INITIAL_HEALTH / 4,

        INITIAL_AMOUNT = 0,

        TIME_TO_COMPLETE_HEALING = 3
    }

    // Stamina related constants
    public enum Stamina
    {
        INITIAL_STAMINA = 100,

        RECOVER_SPEED = INITIAL_STAMINA / 10,
        DASH_STAMINA_CONSUME = 35
    }

    // Values the character speed may assume
    public enum Speed
    {
        RUN_SPEED = 10,
        
        DASH_SPEED = 3 * RUN_SPEED,
        HEALING_SPEED = RUN_SPEED / 2
    }

    // Attack related constants
    public enum Attack
    {
        INITIAL_DAMAGE = 10
    }


    /* End of Enums declarations */


    // Declaration and Initialization of each states classes
    MainCharacter_BaseState currentState;

    MainCharacter_BaseState IdleState    = new MainCharacter_IdleState();
    MainCharacter_BaseState RunState     = new MainCharacter_RunState();
    MainCharacter_BaseState DashState    = new MainCharacter_DashState();
    MainCharacter_BaseState AttackState  = new MainCharacter_AttackState();
    MainCharacter_BaseState HealState    = new MainCharacter_HealState();
    // MainCharacter_BaseState ProtectState = new MainCharacter_ProtectState();
    MainCharacter_BaseState OnHitState   = new MainCharacter_OnHitState();
    MainCharacter_BaseState DeadState    = new MainCharacter_DeadState();



    // Variables
    public Camera mainCamera;
    public Transform attackPoint;


    int maxHealth;
    [SerializeField] int currentHealth;

    int currentHealAmount;
    [SerializeField] int healingPotionsCounter;

    float maxStamina;
    [SerializeField] float currentStamina;

    float currentSpeed;

    int currentDamage;



    /**********************/
    /* Main Unity Methods */
    /**********************/


    void Start()
    {
        // Set variables
        maxHealth = (int)Health.INITIAL_HEALTH;
        currentHealth = (int)Health.INITIAL_HEALTH;

        currentHealAmount = (int)HealingPotion.INITIAL_HEAL_AMOUNT;
        healingPotionsCounter = (int)HealingPotion.INITIAL_AMOUNT;

        maxStamina = (float)Stamina.INITIAL_STAMINA;
        currentStamina = (float)Stamina.INITIAL_STAMINA; 

        currentDamage = (int)Attack.INITIAL_DAMAGE;

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
            // case States.PROTECT: currentState = ProtectState; break;
            case States.ON_HIT: currentState = OnHitState; break;
            case States.DEAD: currentState = DeadState; break;
            default: Debug.Log("State not included in ChangeState method!"); break;
        }
        currentState.Enter(this);
    }


    public void RecoverStamina()
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + ((int)Stamina.RECOVER_SPEED * Time.deltaTime));
    }


    public bool ConsumeStamina()
    {
        if (currentStamina < (int)Stamina.DASH_STAMINA_CONSUME)
        {
            return false;
        }

        currentStamina -= (int)Stamina.DASH_STAMINA_CONSUME;
        return true;
    }


    public void TakeDamage(int amount)
    {
        currentHealth = Math.Max(0, currentHealth - amount/2);

        if (currentHealth == 0)
            ChangeState(States.DEAD);
        else
            ChangeState(States.ON_HIT);
    }


    public void Heal()
    {
        currentHealth = Math.Min(maxHealth, currentHealth + currentHealAmount);
        healingPotionsCounter--;
    }

    
    public void SetSpeed(Speed speed)
    {
        currentSpeed = (int)speed;
    }


    /* The next methods must be used to move the character. */
    /* The'll return true if the character moves            */
    public bool MoveUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, currentSpeed * Time.deltaTime, 0);

            return true;
        }
        return false;
    }


    public bool MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
            }

            return true;
        }
        return false;
    }


    public bool MoveDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -currentSpeed * Time.deltaTime, 0);

            return true;
        }
        return false;
    }


    public bool MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(-currentSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            }

            return true;
        }
        return false;
    }


    /***************/
    /* Get Methods */
    /***************/


    public int Get_HealingPotionsCounter()
    {
        return healingPotionsCounter;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(10);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.7f);
    }
}
