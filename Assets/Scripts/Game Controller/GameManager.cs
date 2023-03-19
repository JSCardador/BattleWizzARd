using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///  Enum with the different states in which the game can be.
    /// </summary>
    public enum GameState
    {
        MainMenu,
        ImageSearch,
        InGame,
        GameWon,
        GameOver
    }


    /// <summary>
    ///  The current state of the game.
    /// </summary>
    /// <value></value>
    public GameState CurrentGameState
    {
        private set
        {
            CurrentGameState = value;
            Controller();
        }
        get
        {
            return CurrentGameState;
        }
    }


    /// <summary>
    ///  Sets the current game state.
    /// </summary>
    /// <param name="gameState"></param>
    public void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;
    }


    /// <summary>
    /// The controller for the game states.
    /// From here we will control what will happen in each state of the game.
    /// </summary>
    public void Controller()
    {
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                break;

            case GameState.ImageSearch:
                break;

            case GameState.InGame:
                break;

            case GameState.GameWon:
                break;

            case GameState.GameOver:
                break;

            default:
                break;
        }
    }

}
