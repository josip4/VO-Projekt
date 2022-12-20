using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject options;

    void Start()
    {
      gameOver = false;
      if (options != null)
        options.SetActive(false);
    }

    void Update()
    {
      if (gameOver == true) {
        if (options != null)
          options.SetActive(true);
      }
    }

    public void StartGame()
    {
      SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
