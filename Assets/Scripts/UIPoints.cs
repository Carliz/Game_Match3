using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPoints : MonoBehaviour
{
    [SerializeField] private int displayPoints = 0;
    [SerializeField] private TextMeshProUGUI pointsLabel;

    private void Start()
    {
        GameManager.Instance.OnPointsUpdated.AddListener(UpdatePoints);
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
