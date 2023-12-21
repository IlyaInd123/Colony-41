using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] int nextLevelIndex = 1;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelClearPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
    }

    public void LevelClear()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        levelClearPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevelIndex);
    }
}