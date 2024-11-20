using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectsOnGameState : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameManager.GameState showOnState;

    private void Start()
    {
        target.SetActive(showOnState == GameManager.Instance.gameState);    
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        target.SetActive(showOnState == GameManager.Instance.gameState);      
    }
}
