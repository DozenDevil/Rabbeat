using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Quitted successfully!");
        Application.Quit();
    }
}
