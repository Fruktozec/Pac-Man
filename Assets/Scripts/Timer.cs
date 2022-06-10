using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public IEnumerator CountdownToStart()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 4.2f;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
}
