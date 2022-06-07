using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private string newGameScene;
    [SerializeField] GameManager gameManager;
    public void goToMenu()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void restartGame()
    {
        gameManager.NewGame();

        gameObject.SetActive(false);
    }

    public void advertising()
    {
        gameManager.lives += 1;
        gameManager.SetLives(1);
        gameManager.ResetState();

        gameObject.SetActive(false);
    }
}
