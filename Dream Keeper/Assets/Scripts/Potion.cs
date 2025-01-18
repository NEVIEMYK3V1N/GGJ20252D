using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Potion : MonoBehaviour
{
    public const int MAX_CAPACITY = 5;

    [SerializeField] public Gun _gun;
    [SerializeField] public Color _color;
    private int _currentCapacity = MAX_CAPACITY;
    private float _refillCd = 3; // second
    private bool _isReloading = false;

    private void OnMouseDown()
    {
        Debug.Log("clicked potion: " + this._color.ToString());

        if (this._isReloading)
        {
            return;
        }

        bool load_success = this._gun.fillIn(this._color);

        if (load_success)
        {
            this._currentCapacity--;
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
    }
}
