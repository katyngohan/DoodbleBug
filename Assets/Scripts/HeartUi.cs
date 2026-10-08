
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public GameManager gameManager;
    public Image[] hearts;

    private void Update()
    {
        if (gameManager == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].enabled = i < gameManager.lives;
            }
        }
    }
}
