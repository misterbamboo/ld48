using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHudController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private bool isActive;

    private void Start()
    {
        isActive = false;
        gameOverPanel.SetActive(isActive);
    }

    private void Update()
    {
        if (Game.Instance.State == GameState.GameOver)
        {
            isActive = true;
            gameOverPanel.SetActive(isActive);
        }
    }

    public void Retry()
    {
        if (isActive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
