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

    private YandexGame sdk;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            highScoreText.text = "Рекорд: " + score.ToString();
        }

        if(AudioListener.volume == 0f)
        {
            soundOnObject.SetActive(false);
            soundOffObject.SetActive(true);
        }
        else
        {
            soundOnObject.SetActive(true);
            soundOffObject.SetActive(false);
        }
    }

    private void Start()
    {
        //sdk._FullscreenShow();
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
