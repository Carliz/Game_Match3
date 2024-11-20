using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public float timeToMatch = 10f;
    [SerializeField] public float currentTimeToMatch = 0f;

    public int points = 0;
    public UnityEvent OnPointsUpdated;
    public UnityEvent<GameState> OnGameStateUpdated; //recibe un parametro de tipo GameState

    public enum GameState
    {
        Idle,
        LevelSelection,
        InGame,
        GameOver
    }

    public GameState gameState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int newPoints)
    {
        points += newPoints;
        OnPointsUpdated?.Invoke(); //? para ver si existe, si un obj se suscribio ejecuta el codigo
        currentTimeToMatch = 0f;
    }

    private void Update()
    {
        if(gameState == GameState.InGame)
        {
            currentTimeToMatch += Time.deltaTime;
            if(currentTimeToMatch > timeToMatch)
            {
                gameState = GameState.GameOver;
                //cada vez que cambie el estado del juego otros elementos se pueden suscribir
                OnGameStateUpdated?.Invoke(gameState);
            }
        }
    }

    public void StartGame()
    {
        points = 0;
        gameState = GameState.InGame;
        OnGameStateUpdated?.Invoke(gameState);
        currentTimeToMatch = 0f;
    }

    public void RestartGame()
    {
        points = 0;
        gameState = GameState.InGame;
        OnGameStateUpdated?.Invoke(gameState);
        currentTimeToMatch = 0f;
    }

    public void ExitGame()
    {
        gameState = GameState.Idle;
        points = 0;
        OnGameStateUpdated?.Invoke(gameState);
    }

    public void LevelSelection()
    {
        gameState = GameState.LevelSelection;
        OnGameStateUpdated?.Invoke(gameState);
    }

    public void Idle()
    {
        gameState = GameState.Idle;
        OnGameStateUpdated?.Invoke(gameState);
    }
}
