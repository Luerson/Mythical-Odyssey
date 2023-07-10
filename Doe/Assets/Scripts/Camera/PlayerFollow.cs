using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] MainCharacter_StateManager player;
    Transform playerTransform;


    private void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }


    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);

        if (transform.position.x > 30)
        {
            transform.position = new Vector3(30, transform.position.y, -10);
        }
        else if (transform.position.x < -30)
        {
            transform.position = new Vector3(-30, transform.position.y, -10);
        }

        if (transform.position.y > 20)
        {
            transform.position = new Vector3(transform.position.x, 20, -10);
        }
        else if (transform.position.y < -20)
        {
            transform.position = new Vector3(transform.position.x, -20, -10);
        }
    }
}
