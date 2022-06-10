using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource startSound;

    [SerializeField] private AudioClip ghostEatenSound;
    [SerializeField] private AudioClip gameSound;
    [SerializeField] private AudioClip pelletEatenSound;
    [SerializeField] private AudioClip powerPelletEatenSound;
    [SerializeField] private AudioClip overSound;

    [SerializeField] private Timer timer;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    [SerializeField] GameOverPanel overPanel;
    [SerializeField] TouchControl touchPanel;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int highScore { get; private set; }
    public int lives { get; set; }

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("Score"))
        {
            highScore = PlayerPrefs.GetInt("Score");
        }
    }

    private void Start()
    {
        NewGame();
    }

    public void AudioPlay(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public void AudioStop(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Stop();
    }

    public void NewGame()
    {
        overPanel.gameObject.SetActive(false);
        touchPanel.gameObject.SetActive(true);

        StartCoroutine(timer.CountdownToStart());
        startSound.Play();

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    public void NewRound()
    {
        

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    public void ResetState()
    {
        if(lives == 3 && score == 0)
        {
            _audioSource.clip = gameSound;
            _audioSource.PlayDelayed(4.2f);
        }
        else
        {
            _audioSource.clip = gameSound;
            _audioSource.Play();
        }        

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        overPanel.gameObject.SetActive(true);
        touchPanel.gameObject.SetActive(false);

        AudioStop(gameSound);
        AudioStop(powerPelletEatenSound);

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');

        HighScore();
    }

    public void HighScore()
    {
        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("Score", highScore);
        }
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            Invoke(nameof(GameOver), 3.0f);
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        _audioSource.PlayOneShot(ghostEatenSound);

        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);
        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        _audioSource.PlayOneShot(pelletEatenSound);

        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            //_audioSource.clip = overSound;
            //_audioSource.PlayScheduled(3f);
            _audioSource.Stop();
            _audioSource.PlayOneShot(overSound);

            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        AudioStop(gameSound);
        AudioPlay(powerPelletEatenSound);


        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        AudioStop(powerPelletEatenSound);
        AudioPlay(gameSound);

        ghostMultiplier = 1;
    }
}


