using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public int damageAmount;
    public int currentHealth;
    public GameObject XP_Orb;

    float currentTime;
    Transform player;
    OrbSpawner orbSpawner;
    PauseScript Pause;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        orbSpawner = GameObject.FindGameObjectWithTag("XP").GetComponent<OrbSpawner>();
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseScript>();
        currentTime = Time.time;
    }

    private void Update()
    {
        if (!Pause.Paused())
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);

            Vector3 scale = new Vector3((direction.x > 0 ? -1 : 1), 1.0f, 1.0f);
            transform.localScale = scale;

            if (Time.time > currentTime + 0.25f)
            {
                transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCharacter_StateManager playerHealth = other.GetComponent<MainCharacter_StateManager>();
            if (playerHealth.Get_CurrentHealth() != 0)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Vector3 position = transform.position;
            orbSpawner.GeraOrb(position);
            Destroy(gameObject);
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().color = Color.yellow;
            currentTime = Time.time;
        }
    }
}
