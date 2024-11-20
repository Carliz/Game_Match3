using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UILevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject levelListContainer;
    [SerializeField] private int levelAmount;

    private void Start()
    {
        for (int i = 0; i < levelAmount; i++)
        {
            var newObject = Instantiate(levelButtonPrefab, levelListContainer.transform);
            newObject.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i.ToString();
            newObject.GetComponent<Button>().onClick.AddListener(LevelButtonClicked);
        }
    }

    private void LevelButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void BackButtonPressed()
    {
        GameManager.Instance.Idle();
    }
}
