using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject countdownTime;
    public AudioSource startMusic;
    private void Start()
    {
        //StartCoroutine(CountdownToStart());
        startMusic.Play();
    }
    public IEnumerator CountdownToStart()
    {
        //while (countdownTime > 0)
        //{
        //    countdownDisplay.text = countdownTime.ToString();
        //    yield return new WaitForSeconds(1f);
        //    countdownTime--;
        //}
        //countdownDisplay.text = "GO!";

        

        //yield return new WaitForSeconds(1f);

        //countdownDisplay.gameObject.SetActive(false);

        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 4.2f;
        while(Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }
        countdownTime.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
