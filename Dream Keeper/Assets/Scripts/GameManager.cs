using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using UnityEngine;

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
    private SceneType _gameState = SceneType.Start;

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
        if (this._gameState != SceneType.Start)
        {
            return;
        }

        Debug.Log("Game Started");
        //SceneManager.LoadScene(Enum.GetName(typeof(SceneType), SceneType.Start));
        this._spawnManager.startSpawning();
        this._gameState = SceneType.Game;
    }

    public void endGame()
    {
        if (this._gameState != SceneType.Game)
        {
            return;
        }

        Debug.Log("Game Ended");
        this._gameState = SceneType.End;
        this._spawnManager.stopSpawning();
        this._spawnManager._monsterManager.ResetManager();

        //SceneManager.LoadScene(Enum.GetName(typeof(SceneType), SceneType.End));
    }

    public void toStartMenu()
    {
        if (this._gameState != SceneType.End)
        {
            return;
        }

        Debug.Log("To Start Menu");
        this._gameState = SceneType.Start;
    }
}

