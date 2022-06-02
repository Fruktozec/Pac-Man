using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Text highScoreText;
    [SerializeField] private int score;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            highScoreText.text = "Рекорд: " + score.ToString();
        }
    }

    private void startNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    private void soundOn()
    {
        audioSource.mute = true;
    }

    private void soundOff()
    {
        audioSource.mute = false;
    }
}
