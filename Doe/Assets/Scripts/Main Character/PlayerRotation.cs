using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] MainCharacter_StateManager player;


    void Update()
    {
        Vector3 mousePosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x >= player.transform.position.x)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
