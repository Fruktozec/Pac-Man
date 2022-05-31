using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public AudioSource _audioSource;

    public AudioClip startMusic;
    public AudioClip ghostEatenSound;
    public AudioClip gameSound;
    public AudioClip pelletEatenSound;
    public AudioClip powerPelletEatenSound;
    public AudioClip overSound;

    public Timer timer;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int highScore { get; private set; }
    public int lives { get; private set; }

    public static GameManager instance;

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

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) 
        {
            NewGame();
        }
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
        gameOverText.enabled = false;

        StartCoroutine(timer.CountdownToStart());

        //startMusic.Play();
        //_audioSource.clip = startMusic;
        //_audioSource.Play();

        AudioPlay(startMusic);

        //StartCoroutine(delay());

        //_audioSource.clip = gameSound;
        //_audioSource.PlayDelayed(4.2f);

        AudioPlay(overSound);

        SetScore(0);
        SetLives(3);
        NewRound();
    }
    public IEnumerator delay()
    {
        yield return new WaitForSeconds(4.2f);
        _audioSource.clip = gameSound;
        _audioSource.Play();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        //gameSound.Stop();
        //powerPelletEatenSound.Stop();

        AudioStop(gameSound);
        AudioStop(powerPelletEatenSound);

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
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
        if(score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("Score", highScore);
        }
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();

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
        //ghostEatenSound.Play();
        AudioPlay(ghostEatenSound);

        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);
        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        //pelletEatenSound.Play();
        _audioSource.PlayOneShot(pelletEatenSound);

        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            //pelletEatenSound.Stop();
            //overSound.PlayScheduled(3f);
            _audioSource.clip = overSound;
            _audioSource.PlayScheduled(3f);
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
        //powerPelletEatenSound.Play();
        //gameSound.Stop();

        AudioPlay(powerPelletEatenSound);
        AudioStop(gameSound);

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
        //powerPelletEatenSound.Stop();
        //gameSound.Play();

        AudioStop(powerPelletEatenSound);
        AudioPlay(gameSound);

        ghostMultiplier = 1;
    }
}


