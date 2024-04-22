using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    Loading,
    CutScene,
    Playing,
    Pause,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private EGameState GameState = EGameState.Loading;

    public event Action<EGameState> OnGameStateChanged;

    public void UpdateGameState(EGameState newState)
    {
        GameState = newState;

        OnGameStateChanged?.Invoke(newState);
    }
}
