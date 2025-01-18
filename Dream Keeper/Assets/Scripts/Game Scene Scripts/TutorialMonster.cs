using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMonster : MonoBehaviour
{
    //[SerializeField] GameManager _gameManager;

    void Start()
    {
        MonsterManager.Instance.AddMonster(this.gameObject);
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
        GameManager.Instance.setGameState(SceneType.Start);
    }
}
