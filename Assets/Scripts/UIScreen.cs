using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private CanvasGroup containerCanvas;
    [SerializeField] private Image background;
    [SerializeField] private GameManager.GameState visibleState;
    [SerializeField] private float transitionTime;
    [SerializeField] private float visibleAlpha = 1;

    private void Start()
    {
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
        bool initialState = GameManager.Instance.gameState == visibleState;
        background.enabled = initialState;
        containerRect.gameObject.SetActive(initialState);
    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        if(newState == visibleState) //mostrar la pantalla de forma animada
        {
            ShowScreen();
        }
        else
        {
            HideScreen();
        }
    }

    private void HideScreen()
    {
        //background animation
        var bgColor = background.color;
        bgColor.a = 0;
        background.DOColor(bgColor, transitionTime * 0.5f);
        //container animation
        containerCanvas.alpha = visibleAlpha;
        containerRect.anchoredPosition = Vector2.zero;
        containerCanvas.DOFade(0f, transitionTime * 0.5f);
        containerRect.DOAnchorPos(new Vector2(0, -100), transitionTime * 0.5f).onComplete = () =>
        {
            background.enabled = false;
            containerRect.gameObject.SetActive(false);
        };
    }

    private void ShowScreen()
    {
        //activa los elementos
        background.enabled = true;
        containerRect.gameObject.SetActive(true);
        //background animation
        var bgColor = background.color;
        bgColor.a = 0;
        background.color = bgColor;
        bgColor.a = visibleAlpha;
        background.DOColor(bgColor, transitionTime);
        //container animation
        containerCanvas.alpha = 0;
        containerRect.anchoredPosition = new Vector2(0, 100); // sube 100 pixeles hacia arriba
        containerCanvas.DOFade(1, transitionTime);
        containerRect.DOAnchorPos(Vector2.zero, transitionTime);//hace la transicion
    }
}
