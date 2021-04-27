using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            canvas.enabled = true;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.enabled)
        {
            Game.Instance.InGameMenu = true;
            canvas.enabled = true;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
        {
            SceneManager.LoadScene(0);
        }

        Application.Quit();        
    }
}
