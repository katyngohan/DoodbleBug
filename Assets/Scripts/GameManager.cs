using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Ant ant;

    public int score { get; private set; }
    public int lives { get; private set; }
    public int keys { get; private set; }

    public GameObject winText;
    public GameObject gameOverText;

    private bool gameEnded = false;

    
    [Header("Menu")]
    public PlayButtonUI menuUI;
    public float returnDelay = 3f;


    private void Start()
    {
        NewGame();
    }



    private void NewGame()
    {
        // Unfreeze game
        Time.timeScale = 1f;

        gameEnded = false;

        SetScore(0);
        SetLives(3);
        keys = 0;

        // Hide WIN text
        if (winText != null)
        {
            winText.SetActive(false);
        }

        // Hide GAME OVER text
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        NewRound();
    }

    private void NewRound()
    {
        // Reset all ghosts
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        // Reset Ant
        ant.gameObject.SetActive(true);
        ant.ResetState();
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void AddLife()
    {
        SetLives(lives + 1);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + ghost.points);
    }

    public void AntEaten()
    {
        // Don't kill Ant again if game already ended
        if (gameEnded)
            return;

        ant.gameObject.SetActive(false);

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void AddKey()
    {
        if (gameEnded)
            return;

        keys++;

        if (keys >= 3)
        {
            WinGame();
        }
    }

    private void WinGame()
{
    if (gameEnded) return;

    gameEnded = true;

    if (winText != null)
        winText.SetActive(true);

    Time.timeScale = 0f;

    StartCoroutine(ReturnToMenuAfterDelay());
}

private void GameOver()
{
    if (gameEnded) return;

    gameEnded = true;

    if (gameOverText != null)
        gameOverText.SetActive(true);

    Time.timeScale = 0f;

    StartCoroutine(ReturnToMenuAfterDelay());
}

private System.Collections.IEnumerator ReturnToMenuAfterDelay()
{
    yield return new WaitForSecondsRealtime(returnDelay);

    if (menuUI != null)
        menuUI.ReturnToMenu();
}

    
public void StartNewGame()
{
    CancelInvoke();
    NewGame();
}

}