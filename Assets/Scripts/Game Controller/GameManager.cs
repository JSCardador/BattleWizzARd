using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    /// <summary>
    ///  Enum with the different states in which the game can be.
    /// </summary>
    public enum GameState
    {
        MainMenu,
        ImageSearch,
        InGame,
        Pause,
        GameWon,
        GameOver
    }


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


    private void Start()
    {
        CurrentGameState = GameState.MainMenu;
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
    private void Controller()
    {
        switch (CurrentGameState)
        {
            case GameState.MainMenu:

                break;

            case GameState.ImageSearch:
                break;

            case GameState.InGame:
                OnResumeGame();
                break;

            case GameState.Pause:
                OnPauseGame();
                break;

            case GameState.GameWon:
                break;

            case GameState.GameOver:
                break;

            default:
                break;
        }
    }


    private void OnPauseGame()
    {
        Time.timeScale = 0;
    }

    private void OnResumeGame()
    {
        Time.timeScale = 1;
    }
}
