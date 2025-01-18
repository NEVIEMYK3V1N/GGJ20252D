using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    const int MAX_CAPACITY = 4;
    private Color[] _colors = new Color[MAX_CAPACITY];

    [SerializeField] MonsterManager _monsterManager;
    [SerializeField] GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool fillIn(Color color)
    {
        if (_colors.Count() >= MAX_CAPACITY)
        {
            return false;
        }
        
        this._colors.Append(color);
        return true;
    }

    public void shoot()
    {
        bool has_monster = this._monsterManager.HasMonster(this._colors);
        if (has_monster)
        {
            int score = this._monsterManager.DestroyMonster(this._colors);
            this._gameManager.addScore(score);
        }
        this._colors = new Color[MAX_CAPACITY];
    }
}
