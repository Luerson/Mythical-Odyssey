using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistMovement : MonoBehaviour
{
    public float Speed;
    public float Money;
    public float XP;
    public float TotalLife;
    public float TotalEnergy;
    public float EnergyRecovery;
    public float ProtectionDefense;
    public float HealingPotions;

    private Rigidbody2D Rigidbody2D;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        //float Vertical = Input.GetAxisRaw("Vertical");
        Vector2 Direction = new Vector2(Horizontal, 0.0f);

        if (Horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (Horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        Rigidbody2D.velocity = Direction * Speed;
    }
}
