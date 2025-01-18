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
    [SerializeField] public SpawnManager _spawnManager;

    // Static property for cross-scene score sharing
    private int _score;

    public void addScore(int score)
    {
        this._score += score;
    }

    public int getScore()
    {
        return this._score;
    }

    public void startGame()
    {
        Debug.Log("Game Started");
        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), SceneType.Start));
        this._spawnManager.startSpawning();
    }

    public void endGame()
    {
        Debug.Log("Game Ended");
        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), SceneType.End));
    }
}

