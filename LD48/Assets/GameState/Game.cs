using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGame
{
    bool GameOver { get; }
}

public class Game : MonoBehaviour, IGame
{
    public static IGame Instance { get; set; }

    public bool GameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Life.Instance.IsDead)
        {
            GameOver = true;
        }
    }
}
