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

    public float _speed = 0.02f;
    public float _moveCd = 0.02f;

    void Start()
    {
        startTime = Time.time; // 记录开始时间
        //Debug.Log("Monster " + _monsterName + " has been created");

        StartCoroutine(CheckSurvival()); // 启动协程来检查生存周期
        StartCoroutine(randomMove());
    }


    public void InitialMonster(String monsterName, Color[] colors, int score, Sprite sprite)
    {
        this._monsterName = monsterName;
        this._colors = colors;
        this._score = score;
        this._spriteRenderer.sprite = sprite;
    }

    public void InitialMonster(String monsterName, Color[] colors, int score)
    {
        this._monsterName = monsterName;
        this._colors = colors;
        this._score = score;
    }

    private IEnumerator CheckSurvival()
    {
        yield return new WaitForSeconds(timer); // 等待timer秒

        OnGameOver();

    }

    public void OnGameOver()
    {
        //Debug.Log("Game Over: Monster " + _monsterName + " has been defeated in time");

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            GameManager.Instance.setGameState(SceneType.End);
        }
    }



    public IEnumerator randomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(this._moveCd);

            float dx = UnityEngine.Random.Range(-1, 2) * this._speed;
            float dy = UnityEngine.Random.Range(-1, 2) * this._speed;

            Vector3 moveDelta = new Vector3(dx, dy, 0);

            this.gameObject.transform.Translate(moveDelta);
        }
    }
}





