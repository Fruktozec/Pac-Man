using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene;

    [SerializeField] private AudioSource audioSource;

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
