
using UnityEngine;

public class PlayButtonUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameTimer gameTimer;
    public GameObject gameplayObjects;
    public GameManager gameManager;

    private void Start()
    {
        ReturnToMenu();
    }

    public void PlayGame()
    {
        if (gameplayObjects != null)
            gameplayObjects.SetActive(true);

        if (mainMenu != null)
            mainMenu.SetActive(false);

        // Reset the game before starting
        if (gameManager != null)
            gameManager.StartNewGame();

        Time.timeScale = 1f;

        if (gameTimer != null)
            gameTimer.RestartTimer();
    }

   public void ReturnToMenu()
{
    if (gameManager != null)
    {
        gameManager.StopAllCoroutines();
        gameManager.CancelInvoke();

        if (gameManager.winText != null)
            gameManager.winText.SetActive(false);

        if (gameManager.gameOverText != null)
            gameManager.gameOverText.SetActive(false);
    }

    Time.timeScale = 0f;

    if (gameplayObjects != null)
        gameplayObjects.SetActive(false);

    if (mainMenu != null)
        mainMenu.SetActive(true);

    if (gameTimer != null)
        gameTimer.ResetTimer();
}
}
