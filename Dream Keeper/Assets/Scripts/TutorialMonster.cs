using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonster : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;

    void Start()
    {
        this._gameManager._spawnManager._monsterManager.AddMonster(this.gameObject);
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
        this._gameManager.startGame();
    }
}
