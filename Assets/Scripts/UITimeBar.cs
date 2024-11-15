using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBar : MonoBehaviour
{
    [SerializeField] private RectTransform fillRect;
    [SerializeField] private Image fillColor;
    [SerializeField] private Gradient gradient;

    private void Update()
    {
        float factor = GameManager.Instance.currentTimeToMatch / GameManager.Instance.timeToMatch;
        factor = Mathf.Clamp(factor, 0f, 1f);
        factor = 1 - factor;
        fillRect.localScale = new Vector3(factor, 1f, 1f);
        fillColor.color = gradient.Evaluate(factor);
    }
}
