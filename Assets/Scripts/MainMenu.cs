using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Text highScoreText;
    public int score;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            highScoreText.text = "������: " + score.ToString();
        }
    }

    private void Update()
    {
        //GameObject a = GameObject.Find(nameof(GameManager));
        //GameManager gameManager = a.GetComponent<GameManager>();
        //GameManager.instance = gameManager;
        //GetComponent<Text>().text = "������: " + gameManager.highScore;
    }

    public void startNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void soundOn()
    {
        audioSource.mute = true;
    }

    public void soundOff()
    {
        audioSource.mute = false;
    }
}
