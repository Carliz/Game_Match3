using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartScreen : MonoBehaviour
{
    public void StartButtonClicked()
    {
        GameManager.Instance.LevelSelection();
    }

    public void OptionsButtonClicked()
    {
        GameManager.Instance.Options();
    }
}
