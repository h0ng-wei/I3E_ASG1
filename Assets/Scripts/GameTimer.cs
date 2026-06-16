// =============================================================================
// GameTimer.cs
// Tracks the total time elapsed since the game started and provides methods
// to stop the timer and retrieve the time in a formatted MM:SS string.
// =============================================================================

using UnityEngine;

public class GameTimer : MonoBehaviour
{
    /// <summary>The total time elapsed since the game started in seconds.</summary>
    public static float timeElapsed = 0f;

    /// <summary>Whether the timer is currently running.</summary>
    private static bool timerRunning = true;

    /// <summary>Increments the timer every frame while it is running.</summary>
    void Update()
    {
        if (timerRunning)
            timeElapsed += Time.deltaTime;
    }

    /// <summary>Stops the timer from incrementing.</summary>
    public static void StopTimer()
    {
        timerRunning = false;
    }

    /// <summary>Returns the elapsed time formatted as MM:SS.</summary>
    public static string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}