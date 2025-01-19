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
using System.Linq;

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

    const int MAX_TROPHYS = 3;

    [SerializeField] public GameObject[] _emptyTrophysPrefabs = new GameObject[MAX_TROPHYS];
    [SerializeField] public GameObject[] _trophysPrefabs = new GameObject[MAX_TROPHYS];
    [SerializeField] public int[] _trophyUnlockReqs = new int[MAX_TROPHYS] {50, 100, 200};


    private bool[] _trophyUnlocked = new bool[MAX_TROPHYS] { false, false, false};
    private GameObject[] _trophys = new GameObject[MAX_TROPHYS];

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
        for (int i = 0; i < GameManager.Instance._trophyUnlockReqs.Count(); i++)
        {
            if (GameManager.Instance._trophyUnlocked[i] == false && 
                GameManager.Instance._score >= GameManager.Instance._trophyUnlockReqs[i])
            {
                GameManager.Instance._trophyUnlocked[i] = true;
                renderTrophy();
            }
        }
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
            var bedroomAnim = GameObject.Find("BedroomAnim");
            bedroomAnim.GetComponent<BedroomAnim>().SwitchAnimation();
            StartCoroutine(GameManager.Instance.loadEndGame(bedroomAnim.GetComponent<BedroomAnim>().GetAnimationLength()));
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
        //GameManager.Instance.addScore(200);
        StartCoroutine(GameManager.Instance.renderTrophy());
    }

    public IEnumerator loadEndGame(float seconds)
    {
        Debug.Log("Waiting for " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);
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

    private IEnumerator renderTrophy()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < GameManager.Instance._trophyUnlocked.Count(); i++)
        {
            if (GameManager.Instance._trophyUnlocked[i])
            {
                GameObject trophy = (GameObject)Instantiate(GameManager.Instance._trophysPrefabs[i]);
                if (GameManager.Instance._trophys[i] != null)
                {
                    Destroy(GameManager.Instance._trophys[i]);
                }
                GameManager.Instance._trophys[i] = trophy;
            }
            else if (GameManager.Instance._trophys[i] == null)
            {
                GameObject trophy = (GameObject)Instantiate(GameManager.Instance._emptyTrophysPrefabs[i]);
                GameManager.Instance._trophys[i] = trophy;
                //Debug.Log("initialized empty trophy: " + GameManager.Instance._trophys.Count());
            }
        }
    }
}

