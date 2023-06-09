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
        FloorSearch,
        ImageSearch,
        InGame,
        Pause,
        GameWon,
        GameOver
    }


   private GameState _currentGameState { get; set; }


    /// <summary>
    ///  The current state of the game.
    /// </summary>
    /// <value></value>
    public GameState CurrentGameState
    {
        private set
        {
            _currentGameState = value;
            Controller();
        }
        get
        {
            return _currentGameState;
        }
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
        UIController.Instance.PanelChanger();

        switch (CurrentGameState)
        {
            case GameState.MainMenu:
                break;

            case GameState.FloorSearch:
                FloorDetect.Instance.StartSearchFloor();
                break;

            case GameState.ImageSearch:
                ImageSearcher.Instance.StartSearchImage();
                break;

            case GameState.InGame:
                OnResumeGame();
                break;

            case GameState.Pause:
                OnPauseGame();
                break;

            case GameState.GameWon:
                OnGameWon();
                break;

            case GameState.GameOver:
                OnGameOver();
                break;

            default:
                break;
        }
    }


    /// <summary>
    /// Pauses the game.
    /// </summary>
    private void OnPauseGame()
    {
        PlayerController.Instance.PauseGame();
        EnemiesManager.Instance.PauseGame();
        Time.timeScale = 0;
    }


    /// <summary>
    /// Resumes the game.
    /// </summary>
    private void OnResumeGame()
    {
        EnemiesManager.Instance.StartGame();
        PlayerController.Instance.StartGame();
        Time.timeScale = 1;
    }


    private void OnGameOver()
    {
        PlayerController.Instance.EndGame();
        EnemiesManager.Instance.EndGame();
        Time.timeScale = 0;
    }


    private void OnGameWon()
    {
        PlayerController.Instance.EndGame();
        EnemiesManager.Instance.EndGame();
        Time.timeScale = 0;
    }

}
