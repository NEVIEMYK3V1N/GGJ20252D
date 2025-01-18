using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Color
{
    Red,
    Green,
    Blue,
    Yellow,
    EMPTY
}

// TODO: unity 三个场景名称，对应 SceneType
public enum SceneType
{
    Start,
    Game,
    End
}



public class GameManager : MonoBehaviour
{
    [SerializeField] public SceneManager _sceneManager;
    [SerializeField] public SpawnManager _spawnManager;

    // Static property for cross-scene score sharing
    public int _score {get; set;}

    public void addScore(int score)
    {
        this._score += score;
    }

    public void startGame()
    {
        Debug.Log("Game Started");
        // TODO
    }

    public void endGame()
    {
        Debug.Log("Game Ended");
        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), SceneType.End));
    }

    // TODO: unity 建一个新场景，期望点击按钮后，跳转到新场景，且新场景显示当前分数5
    public void Test_OnClick()
    {
        this._score = 5;
        endGame();
    }

}


