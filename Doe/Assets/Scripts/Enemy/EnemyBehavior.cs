using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public int damageAmount;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * direction);

        Vector2 scale = new Vector2(-(direction.x/Mathf.Abs(direction.x)), 1.0f);
        transform.localScale = scale;
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
}
