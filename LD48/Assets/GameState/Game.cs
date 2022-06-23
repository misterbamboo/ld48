using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InAction,
    InShop,
    InGameMenu,
    GameOver,
}

public interface IGame
{
    public GameState State { get; }

    public bool Invincible { get; }

    public void ChangeState(GameState state);
}

public class Game : MonoBehaviour, IGame
{
    public static IGame Instance { get; set; }

    public bool Invincible { get; private set; }

    public GameState State { get; private set; }


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
            ChangeState(GameState.GameOver);
        }
    }

    public void ChangeState(GameState state)
    {
        State = state;
    }

    private void ToggleInvincibility()
    {
        Invincible = !Invincible;
    }
}
