using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    public enum TYPE
    {
        HEALTH,
        STAMINA
    }

    public MainCharacter_StateManager player;
    
    public TYPE type;
    float maxSize;


    void Start()
    {
        maxSize = transform.localScale.x;
    }


    void Update()
    {
        float scale = -1;
        if (type == TYPE.HEALTH)
        {
            scale = Get_HealthScale();
        }
        else if (type == TYPE.STAMINA)
        { 
            scale = Get_StaminaScale();
        }

        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    /* Methods for Health and Stamina Types */

    float Get_HealthScale()
    {
        return ((float)player.Get_CurrentHealth() / (float)player.Get_MaxHealth()) * maxSize;
    }

    float Get_StaminaScale()
    {
        return (player.Get_CurrentStamina() / player.Get_MaxStamina()) * maxSize;
    }
}
