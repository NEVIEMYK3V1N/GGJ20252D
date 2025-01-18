using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    const int MAX_CAPACITY = 4;
    private Color[] _colors = new Color[MAX_CAPACITY];
    private int _currentCapacity = 0;

    //[SerializeField] MonsterManager _monsterManager;
    //[SerializeField] GameManager _gameManager;

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

        //debug_printColors();

        return true;
    }

    public void shoot()
    {
        //Debug.Log("Shoot: " + MonsterManager.Instance.GetEliminationCondition(this._colors));

        bool has_monster = MonsterManager.Instance.HasMonster(this._colors);
        if (has_monster)
        {
            //Debug.Log("gun has monster");
            int score = MonsterManager.Instance.DestroyMonster(this._colors);
            GameManager.Instance.addScore(score);
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




    private void debug_printColors()
    {
        string colors = "";
        for (int i = 0; i < MAX_CAPACITY; i++)
        {
            colors += _colors[i] + ", ";
        }
        Debug.Log(colors);
    }
}
