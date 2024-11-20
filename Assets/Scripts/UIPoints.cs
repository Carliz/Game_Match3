using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class UIPoints : MonoBehaviour
{
    [SerializeField] private int displayPoints = 0;
    [SerializeField] private TextMeshProUGUI pointsLabel;
    [SerializeField] private RectTransform icon;

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
        var ogPosition = icon.anchoredPosition;
        var tween = icon.DOShakeAnchorPos(0.25f, 20, 30).SetEase(Ease.InBounce);
        //libreria de tweening para que una animación se repita cierta cnatidad de veces
        tween.SetLoops(-1);
        while (displayPoints < GameManager.Instance.points)
        {
            displayPoints++;
            pointsLabel.text = displayPoints.ToString("D5");
            yield return new WaitForSeconds(0.1f);
        }
        tween.Kill(); //la animación se detiene
        icon.anchoredPosition = ogPosition;
        yield return null;
    }
}
