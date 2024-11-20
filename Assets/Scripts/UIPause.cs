using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    public void ContinueButtonPress()
    {
        GameManager.Instance.ContinueGame();
    }

    public void ExitButtonPress()
    {
        GameManager.Instance.ExitGame();
    }
}
