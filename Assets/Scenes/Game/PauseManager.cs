using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Check for pause input, for example, the "P" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Update the time scale based on the pause state
        if (isPaused)
        {
            // Game is paused
            Time.timeScale = 0f;
        }
        else
        {
            // Game is unpaused
            Time.timeScale = 1f;
        }
    }
}

