using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //public AudioSource music;
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion
    public GameObject gameOverMenu;
    //public bool firstPress = true;

    private void Start()
    {
        //music.Play();
        //music.Pause();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && firstPress)
        //{
        //    music.Play();
        //    firstPress = false;
        //}
    }

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
        //music.Stop();
        //firstPress = true;
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
