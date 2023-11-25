using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public AudioSource music;
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion
    public GameObject gameOverMenu;
    public bool firstPress = true;

    private void LoadSettings()
    {
        music.volume = SettingsMenu.Settings.volume_value;
        music.mute = SettingsMenu.Settings.mute_state;
    }

    private void Start()
    {
        LoadSettings();

        music.Play();
        music.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && firstPress)
        {
            music.Play();
            firstPress = false;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        DamageCollision.OnPlayerDeath += EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        StartCoroutine(SlowDown());
        firstPress = true;
    }

    IEnumerator SlowDown()
    {
        while(music.pitch > 0)
        {
            music.pitch -= 0.025f;
            yield return new WaitForSeconds(0.05f);
        }
        music.Stop(); 
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
