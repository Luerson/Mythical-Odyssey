using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawfordBehavior : MonoBehaviour
{
    public float IdleDistanceReference;
    public GameObject Protagonist;
    public GameObject Canvas;

    private bool MessageInScreen;
    PauseScript Pause;

    private void Start()
    {
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseScript>();
        MessageInScreen = false;
        Canvas.SetActive(false);
    }

    private void Update()
    {
        Vector3 direction = Protagonist.transform.position - transform.position;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        float XDistance = Protagonist.transform.position.x - transform.position.x;
        float YDistance = Protagonist.transform.position.y - transform.position.y;
        float LineDistance = Mathf.Sqrt(XDistance * XDistance + YDistance * YDistance);

        if (LineDistance < IdleDistanceReference && !MessageInScreen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pause.PauseGame();
                Canvas.SetActive(true);
            }

            MessageInScreen = true;
        }
        else
        {
            MessageInScreen = false;
        }
    }
}
