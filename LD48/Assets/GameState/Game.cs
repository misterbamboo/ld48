using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
    bool GameOver { get; }

    bool Invincible { get; }
}

public class Game : MonoBehaviour, IGame
{
    public static IGame Instance { get; set; }

    public bool GameOver { get; private set; }

    public bool Invincible { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInvincibility();
        }

        if (Life.Instance.IsDead)
        {
            GameOver = true;
        }
    }

    private void ToggleInvincibility()
    {
        Invincible = !Invincible;
    }
}
