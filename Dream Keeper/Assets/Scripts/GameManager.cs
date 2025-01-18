using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int _score = 0;

    // Static property for cross-scene score sharing
    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        GameManager.Score = 5;
        endGame();
    }
}

// TODO: unity 三个场景名称，对应 SceneType
public enum SceneType
{
    Start,
    Game,
    End
}
