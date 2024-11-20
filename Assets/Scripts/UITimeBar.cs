using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBar : MonoBehaviour
{
    [SerializeField] private RectTransform fillRect;
    [SerializeField] private Image bar;
    [SerializeField] private Image fillColor;
    [SerializeField] private Gradient gradient;

    private void Start()
    {
        bar.enabled = GameManager.Instance.gameState == GameManager.GameState.InGame;
        fillColor.enabled = GameManager.Instance.gameState == GameManager.GameState.InGame;
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);

    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        bar.enabled = newState == GameManager.GameState.InGame;
        fillColor.enabled = newState == GameManager.GameState.InGame;
    }

    private void Update()
    {
        float factor = GameManager.Instance.currentTimeToMatch / GameManager.Instance.timeToMatch;
        factor = Mathf.Clamp(factor, 0f, 1f);
        factor = 1 - factor;
        fillRect.localScale = new Vector3(factor, 1f, 1f);
        fillColor.color = gradient.Evaluate(factor);
    }
}
