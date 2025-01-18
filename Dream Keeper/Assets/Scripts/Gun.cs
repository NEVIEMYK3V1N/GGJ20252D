using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    const int MAX_CAPACITY = 4;
    private Color[] _colors = new Color[MAX_CAPACITY];
    private int _currentCapacity = 0;

    [SerializeField] MonsterManager _monsterManager;
    [SerializeField] GameManager _gameManager;

    private void Start()
    {
        resetColors();
    }

    private void OnMouseDown()
    {
        shoot();
    }

    public bool fillIn(Color color)
    {
        if (this._currentCapacity >= MAX_CAPACITY)
        {
            return false;
        }
        
        this._colors[_currentCapacity] = color;
        this._currentCapacity++;

        helper_printColors();

        return true;
    }

    private void helper_printColors()
    {
        string colors = "";
        for (int i = 0; i < MAX_CAPACITY; i++)
        {
            colors += _colors[i] + ", ";
        }
        Debug.Log(colors);
    }

    public void shoot()
    {
        Debug.Log("Shoot: " + this._monsterManager.GetEliminationCondition(this._colors));

        bool has_monster = this._monsterManager.HasMonster(this._colors);
        if (has_monster)
        {
            Debug.Log("gun has monster");
            int score = this._monsterManager.DestroyMonster(this._colors);
            //this._gameManager.addScore(score);
            this._gameManager.addScore(score);
        }
        
        resetColors();
    }


    private void resetColors()
    {
        this._currentCapacity = 0;
        this._colors = new Color[MAX_CAPACITY];
        for (int i = 0; i < MAX_CAPACITY; i++)
        {
            this._colors[i] = Color.EMPTY;
        }
    }
}
