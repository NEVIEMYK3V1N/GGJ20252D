using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneTo : MonoBehaviour
{
    [SerializeField] SceneType _sceneFrom;
    [SerializeField] SceneType _sceneTo;

    private void OnMouseDown()
    {
        SwitchSceneTo.switchSceneTo(_sceneFrom, _sceneTo);
    }

    public static void switchSceneTo(SceneType sceneFrom, SceneType sceneTo)
    {
        //SceneManager.UnloadSceneAsync(Enum.GetName(typeof(SceneType), sceneFrom));

        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), sceneTo));
    }
}
