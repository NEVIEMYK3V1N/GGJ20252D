using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneTo : MonoBehaviour
{
    [SerializeField] SceneType _sceneTo;

    private void OnMouseDown()
    {
        SwitchSceneTo.switchSceneTo(_sceneTo);
    }

    public static void switchSceneTo(SceneType sceneTo)
    {
        GameManager.Instance.setGameState(sceneTo);
    }
}
