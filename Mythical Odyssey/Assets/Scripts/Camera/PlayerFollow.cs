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
    }
}
