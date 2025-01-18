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


    private void loadSpawnLogic(string filePath)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            TrimOptions = TrimOptions.Trim,
            Mode = CsvMode.RFC4180,

        };

        // "rg" -> the monster should be destroyed by red + green bubble
        // monsterName, colors, score, spriteRenderer
        // 1,r,1,/Asserts/Images/Monster1.png
        // 2,rg,2,/Asserts/Images/Monster2.png
        using (var reader = File.OpenText(filePath))
        using (var csv = new CsvReader(reader, configuration))
        {
            while (csv.Read())
            {
                var startPoints = csv.GetField<string>(0);
                var endPoints = csv.GetField<string>(1);
                //var colors = ParseColors(colorsStr);
                //var score = csv.GetField<int>(2);
                //var spriteRenderer = csv.GetField<string>(3);

                //Debug.Log("Monster: " + monsterName + " Colors: " + colorsStr + " Score: " + score + " Sprite: " + spriteRenderer);

                //var monsterObj = new MonsterObj
                //{
                //    _monsterName = monsterName,
                //    _colors = colors,
                //    _score = score,
                //    _spritePath = spriteRenderer
                //};

                //_availableMonsters.Add(monsterObj);
            }
        }
    }
}


