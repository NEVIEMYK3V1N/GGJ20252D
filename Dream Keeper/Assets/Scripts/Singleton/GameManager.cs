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
        GameManager.Instance._score += score;
    }

    public int getScore()
    {
        return GameManager.Instance._score;
    }


    public void setGameState(SceneType sceneTo)
    {
        GameManager.Instance._gameState = sceneTo;
        //SceneManager.UnloadSceneAsync(Enum.GetName(typeof(SceneType), sceneFrom));
        SceneManager.LoadScene(Enum.GetName(typeof(SceneType), sceneTo));

        if (sceneTo == SceneType.Start)
        {
            GameManager.Instance.loadStartMenu();
        }
        else if (sceneTo == SceneType.Game)
        {
            GameManager.Instance.loadStartGame();
        }
        else if (sceneTo == SceneType.End)
        {
            //Debug.Log("load end game");
            GameManager.Instance.loadEndGame();
        }
    }

    

    public void startGame()
    {
        Debug.Log("Game Started");

        GameManager.Instance._score = 0;
        SpawnManager.Instance.startSpawning();
        GameManager.Instance._gameState = SceneType.Game;
    }

    public void loadStartGame()
    {
        
    }

    public void loadEndGame()
    {
        Debug.Log("Game Ended");

        AudioManager.Instance.play_audio_success();

        GameManager.Instance._gameState = SceneType.End;
        SpawnManager.Instance.stopSpawning();
        MonsterManager.Instance.ResetManager();
    }

    public void loadStartMenu()
    {
        Debug.Log("To Start Menu");
    }
}

