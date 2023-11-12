using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion
    public GameObject gameOverMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        DamageCollision.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        DamageCollision.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
