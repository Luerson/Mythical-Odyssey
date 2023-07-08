/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed;
    public int damageAmount;

    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthController playerHealth = other.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}*/
