using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static float timeElapsed = 0f;
    private static bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
            timeElapsed += Time.deltaTime;
    }

    public static void StopTimer()
    {
        timerRunning = false;
    }

    public static string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}