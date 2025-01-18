using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] SceneType _sceneTo;

    private void OnMouseDown()
    {
        if (_sceneTo == SceneType.Start)
        {
            this._gameManager.toStartMenu();
        }
        else if (_sceneTo == SceneType.Game)
        {
            this._gameManager.startGame();
        }
        else if (_sceneTo == SceneType.End)
        {
            this._gameManager.endGame();
        }
    }
}
