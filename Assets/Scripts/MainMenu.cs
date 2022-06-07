using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Text highScoreText;
    [SerializeField] private int score;

    [SerializeField] private GameObject soundOnObject;
    [SerializeField] private GameObject soundOffObject;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            highScoreText.text = "Рекорд: " + score.ToString();
        }

        if(AudioListener.volume == 0f)
        {
            soundOffObject.SetActive(false);
            soundOffObject.SetActive(true);
        }
        else
        {
            soundOffObject.SetActive(true);
            soundOffObject.SetActive(false);
        }
    }

    public void startNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void soundOn()
    {
        audioSource.mute = true;
        AudioListener.volume = 0f;
    }

    public void soundOff()
    {
        audioSource.mute = false;
        AudioListener.volume = 1f;
    }
}
