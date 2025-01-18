using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO：unity, 挂在monster预制件上
public class Monster : MonoBehaviour
{
    public String _monsterName = "default";
    public Color[] _colors = new Color[0];
    public int _score = 0;
    public SpriteRenderer _spriteRenderer; // TODO: unity, 把当前的gameobject挂上去，gameobject上需要有个spriteRenderer    
    public int timer = 3; // TODO:unity，可选，生存周期默认为3秒

    private float startTime;

    public GameObject _gameManager; // TODO: unity, 挂在gamemanager上

    void Start()
    {
        startTime = Time.time; // 记录开始时间
        StartCoroutine(CheckSurvival()); // 启动协程来检查生存周期
    }


    public void InitialMonster(String monsterName, Color[] colors, int score, Sprite sprite)
    {
        this._monsterName = monsterName;
        this._colors = colors;
        this._score = score;
        this._spriteRenderer.sprite = sprite;
    }

    private IEnumerator CheckSurvival()
    {
        yield return new WaitForSeconds(timer); // 等待timer秒

        OnGameOver();

    }

    public void OnGameOver()
    {
        Debug.Log("Game Over: Not destroyed in time");

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.endGame();
        }
    }
}

public enum Color
{
    Red,
    Green,
    Blue,
    Yellow
}



