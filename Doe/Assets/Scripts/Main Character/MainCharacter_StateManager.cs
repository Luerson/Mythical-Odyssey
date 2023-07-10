using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainCharacter_StateManager : MonoBehaviour
{
    public AudioClip AttackSound;
    public AudioClip DeathSound;
    public GameObject RestartCanvas;
    Animator Animator;
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

        INITIAL_AMOUNT = 2,

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



    // Variables related to other game objects (maybe children)
    public Camera mainCamera;
    public Transform attackPoint;
    public TextMeshProUGUI potionsText;
    public TextMeshProUGUI xpText;

    // Variables
    Rigidbody2D Rigidbody2D;

    //Pause Object
    PauseScript Pause;


    int maxHealth;
    [SerializeField] int currentHealth;

    int currentHealAmount;
    [SerializeField] int healingPotionsCounter;

    float maxStamina;
    [SerializeField] float currentStamina;

    float currentSpeed;

    int currentDamage;

    public int currentXP;

    /**********************/
    /* Main Unity Methods */
    /**********************/


    void Start()
    {
        // Set variables
        Rigidbody2D = GetComponent<Rigidbody2D>();

        RestartCanvas.SetActive(false);

        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseScript>();

        maxHealth = (int)Health.INITIAL_HEALTH;
        currentHealth = (int)Health.INITIAL_HEALTH;

        currentHealAmount = (int)HealingPotion.INITIAL_HEAL_AMOUNT;
        healingPotionsCounter = (int)HealingPotion.INITIAL_AMOUNT;

        maxStamina = (float)Stamina.INITIAL_STAMINA;
        currentStamina = (float)Stamina.INITIAL_STAMINA; 

        currentDamage = (int)Attack.INITIAL_DAMAGE;

        currentXP = 0;

        potionsText.text = healingPotionsCounter.ToString();
        xpText.text = currentXP.ToString();

        Animator = GetComponent<Animator>();

        // Player will start idle
        ChangeState(States.IDLE);
    }


    void Update()
    {
        if (!Pause.Paused())
            // Update player (regardless the current state, the Update method will handle it by its own)
            currentState.Update();
    }


    private void FixedUpdate()
    {
        currentState.FixedUpdate();
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
        currentHealth = Math.Max(0, currentHealth - amount);

        if (currentHealth == 0)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(DeathSound);
            RestartCanvas.SetActive(true);
            ChangeState(States.DEAD);
            Time.timeScale = 0;
        }
        else
            ChangeState(States.ON_HIT);
    }


    public void Heal()
    {
        currentHealth = Math.Min(maxHealth, currentHealth + currentHealAmount);
        healingPotionsCounter--;

        potionsText.text = healingPotionsCounter.ToString();
    }

    
    public void SetSpeed(Speed speed)
    {
        currentSpeed = (int)speed;
    }


    public void ChangeXP(int amount)
    {
        currentXP += amount;

        xpText.text = currentXP.ToString();
    }

    public void IncreaseMaxHP(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
    }

    public void IncreaseMaxStamina(int amount)
    {
        maxStamina += amount;
        currentStamina = maxHealth;
    }

    public void IncreaseHealingPotionsTotal()
    {
        healingPotionsCounter++;

        potionsText.text = healingPotionsCounter.ToString();
    }

    /* The next methods must be used to move the character. */
    /* The'll return true if the character moves            */


    public bool Move()
    {
        if (!Pause.Paused())
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal < 0.0f && transform.position.x <= -30)
            {
                horizontal = 0.0f;
            } else if (horizontal > 0.0f && transform.position.x >= 30)
            {
                horizontal = 0.0f;
            }

            if (vertical < 0.0f && transform.position.y <= -20)
            {
                vertical = 0.0f;
            }
            else if (vertical > 0.0f && transform.position.y >= 20)
            {
                vertical = 0.0f;
            }


            Rigidbody2D.velocity = new Vector2(horizontal, vertical) * currentSpeed;

            if (horizontal < 0f)
                transform.localScale = new Vector3(1f, 1f, 1f);
            else if (horizontal > 0f)
                transform.localScale = new Vector3(-1f, 1f, 1f);

            Animator.SetBool("Walking", (horizontal != 0f || vertical != 0f));

            return (horizontal != 0f || vertical != 0f);
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

    public int Get_MaxHealth()
    {
        return maxHealth;
    }


    public int Get_CurrentHealth()
    {
        return currentHealth;
    }


    public float Get_MaxStamina()
    {
        return maxStamina;
    }


    public float Get_CurrentStamina()
    {
        return currentStamina;
    }

    public float Get_CurrentXP()
    {
        return currentXP;
    }

    public AudioClip Get_Attack_Clip()
    {
        return AttackSound;
    }


    /****************************/
    /****************************/
    /****************************/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("XP_Orb"))
        {
            ChangeXP(10);
            Destroy(collision.gameObject);
        }
    }


    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.7f);
    }     
     */
}
