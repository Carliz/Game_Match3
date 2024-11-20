using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectsOnGameState : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameManager.GameState hideOnState;

    private void Start()
    {
        if(hideOnState == GameManager.Instance.gameState)
        {
            target.SetActive(false);
        }
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        target.SetActive(hideOnState != newState);
    }
}
