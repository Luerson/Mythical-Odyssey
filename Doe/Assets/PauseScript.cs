using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool paused;

    private void Start()
    {
        paused = false;
    }

    public bool Paused()
    {
        return paused;
    }

    public bool PauseGame()
    {
        paused = true;
        return true;
    }

    public bool Resume()
    {
        paused = false;
        return true;
    }
}
