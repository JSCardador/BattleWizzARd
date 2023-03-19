using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }


    [Header("Panels")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWonScreen;


    [Header("Buttons")]
    [SerializeField] private Button btnPlayGame;
    [SerializeField] private Button btnQuitGame;
    [SerializeField] private Button btnPauseGame;
    [SerializeField] private Button btnResumeGame;
    [SerializeField] private Button btnRestartGame;
    [SerializeField] private Button btnMainMenuGame;
    [SerializeField] private Button btnGameOverGame;


    void Awake()
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


    void Start()
    {
        btnPlayGame.onClick.AddListener(() => GameManager.Instance.SetGameState(GameManager.GameState.ImageSearch));
        btnQuitGame.onClick.AddListener(Application.Quit);
        btnPauseGame.onClick.AddListener(() => GameManager.Instance.SetGameState(GameManager.GameState.Pause));
        btnResumeGame.onClick.AddListener(() => GameManager.Instance.SetGameState(GameManager.GameState.InGame));
        btnMainMenuGame.onClick.AddListener(() => GameManager.Instance.SetGameState(GameManager.GameState.MainMenu));
        btnGameOverGame.onClick.AddListener(() => GameManager.Instance.SetGameState(GameManager.GameState.MainMenu));
        // btnRestartGame.onClick.AddListener(() => GameManager.Instance.CurrentGameState = GameManager.GameState.InGame);
    }

    private void PanelChanger()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case GameManager.GameState.MainMenu:
                mainMenuScreen.SetActive(true);
                inGameScreen.SetActive(false);
                pauseScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                break;
            case GameManager.GameState.ImageSearch:
                mainMenuScreen.SetActive(false);
                inGameScreen.SetActive(true);
                pauseScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                break;
            case GameManager.GameState.InGame:
                mainMenuScreen.SetActive(false);
                inGameScreen.SetActive(true);
                pauseScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                break;
            case GameManager.GameState.Pause:
                mainMenuScreen.SetActive(false);
                inGameScreen.SetActive(false);
                pauseScreen.SetActive(true);
                gameOverScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                break;
            case GameManager.GameState.GameOver:
                mainMenuScreen.SetActive(false);
                inGameScreen.SetActive(false);
                pauseScreen.SetActive(false);
                gameOverScreen.SetActive(true);
                gameWonScreen.SetActive(false);
                break;
            case GameManager.GameState.GameWon:
                mainMenuScreen.SetActive(false);
                inGameScreen.SetActive(false);
                pauseScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                gameWonScreen.SetActive(true);
                break;
            default:
                break;
        }
    }

}