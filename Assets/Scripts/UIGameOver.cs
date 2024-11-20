using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private int displayedPoints = 0;
    [SerializeField] private TextMeshProUGUI pointsUI;

    private void Start()
    {
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
    }

    // para desuscribirnos de un evento cuando los obj son destruidos,
    // recomendado cuando tenemos múltiples escenas
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateUpdated.RemoveListener(GameStateUpdated);
    }

    public void GameStateUpdated(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.GameOver) //si se acabó el juego
        {
            displayedPoints = 0;
            StartCoroutine(DisplayPointsCoroutine());
        }
    }

    private IEnumerator DisplayPointsCoroutine()
    {
        while(displayedPoints < GameManager.Instance.points)
        {
            displayedPoints++;
            pointsUI.text = displayedPoints.ToString();
            yield return new WaitForFixedUpdate();
        }
        displayedPoints = GameManager.Instance.points;
        pointsUI.text = displayedPoints.ToString();
        yield return null;
    }

    public void PlayAgainButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }

    public void ExitGameButtonClicked()
    {
        GameManager.Instance.ExitGame();
    }
}
