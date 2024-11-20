using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPoints : MonoBehaviour
{
    [SerializeField] private int displayPoints = 0;
    [SerializeField] private TextMeshProUGUI pointsLabel;

    private void Start()
    {
        GameManager.Instance.OnPointsUpdated.AddListener(UpdatePoints);
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPointsUpdated.RemoveListener(UpdatePoints);
        GameManager.Instance.OnGameStateUpdated.RemoveListener(GameStateUpdated);

    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.GameOver)
        {
            displayPoints = 0;
            pointsLabel.text = displayPoints.ToString();
        }
    }

    private void UpdatePoints()
    {
        StartCoroutine(UpdatePointsCoroutine());
    }

    private IEnumerator UpdatePointsCoroutine()
    {
        while (displayPoints < GameManager.Instance.points)
        {
            displayPoints++;
            pointsLabel.text = displayPoints.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
