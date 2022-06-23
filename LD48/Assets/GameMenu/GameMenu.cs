using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject inGameCanvas;
    
    [SerializeField]
    Canvas canvas;

    private void Awake()
    {
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvas.enabled)
            {
                Game.Instance.ChangeState(GameState.InGameMenu);
                canvas.enabled = true;
                inGameCanvas.SetActive(false);
            }
            else
            {
                Game.Instance.ChangeState(GameState.InAction);
                canvas.enabled = false;
                inGameCanvas.SetActive(true);
            }

            PauseGame(canvas.enabled);
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

    void PauseGame(bool isGamePause)
    {
        if (isGamePause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
