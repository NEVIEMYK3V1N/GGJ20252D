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
    // [SerializeField] public SpawnManager _spawnManager;

    // Static property for cross-scene score sharing
    private int _score;
    private SceneType _gameState = SceneType.Start;

    // singleton
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    public void addScore(int score)
    {
        this._score += score;
    }

    public int getScore()
    {
        return this._score;
    }


    public void setGameState(SceneType sceneTo)
    {
        this._gameState = sceneTo;
        //SceneManager.UnloadSceneAsync(Enum.GetName(typeof(SceneType), sceneFrom));
        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), sceneTo));

        if (sceneTo == SceneType.Start)
        {
            this.toStartMenu();
        }
        else if (sceneTo == SceneType.Game)
        {
            this.startGame();
        }
        else if (sceneTo == SceneType.End)
        {
            this.endGame();
        }
    }



    public void startGame()
    {
        if (this._gameState != SceneType.Start)
        {
            return;
        }

        Debug.Log("Game Started");

        this._score = 0;
        SpawnManager.Instance.startSpawning();
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
        SpawnManager.Instance.stopSpawning();
        MonsterManager.Instance.ResetManager();
    }

    public void toStartMenu()
    {
        if (this._gameState != SceneType.End)
        {
            return;
        }

        Debug.Log("To Start Menu");
    }
}

