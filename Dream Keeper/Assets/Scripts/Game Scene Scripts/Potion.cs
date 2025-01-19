using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;



public class Potion : MonoBehaviour
{
    public const int MAX_CAPACITY = 5;

    [SerializeField] public Gun _gun;
    [SerializeField] public Color _color;

    [Header("Bottle Charges")]
    [SerializeField] public GameObject[] _potionCharges = new GameObject[MAX_CAPACITY];

    private int _currentCapacity = MAX_CAPACITY;
    private float _refillCd = 1; // second
    private bool _isReloading = false;

    private void OnMouseDown()
    {
        //Debug.Log("clicked potion: " + this._color.ToString());

        AudioManager.Instance.play_audio_bottle_shake();

        if (this._isReloading)
        {
            return;
        }

        bool load_success = this._gun.fillIn(this._color);

        if (load_success)
        {
            this._currentCapacity--;

            this._potionCharges[this._currentCapacity].SetActive(false);

            if (this._currentCapacity == 0)
            {
                this._isReloading = true;
                StartCoroutine(refill());
            }
        }
    }

    private IEnumerator refill()
    {
        yield return new WaitForSeconds(_refillCd); // µÈ´ýtimerÃë
        this._currentCapacity = MAX_CAPACITY;
        this._isReloading = false;

        foreach(var obj in this._potionCharges)
        {
            obj.SetActive(true);
        }
    }
}
