using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum DisplayScoreTextType
{
    Score
}

public class TextControl : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;
    [SerializeField] DisplayScoreTextType _displayScoreTextType;

    private void Start()
    {
        if (this._displayScoreTextType == DisplayScoreTextType.Score)
        {
            this._textMeshPro.text = GameManager.Instance.getScore().ToString();
        }
    }
}
