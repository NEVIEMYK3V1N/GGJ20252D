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

    public float shakeDuration = 0.2f; // 晃动持续时间
    public float shakeMagnitude = 0.01f; // 晃动幅度

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

        StartCoroutine(Shake());

    }

    private System.Collections.IEnumerator Shake()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = this.gameObject.transform.position;
        while (elapsedTime < shakeDuration)
        {
            // 随机生成晃动的偏移量
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            Vector3 moveDelta = new Vector3(offsetX, offsetY, 0);
            this.gameObject.transform.Translate(moveDelta);

            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }
        // 重置位置
        this.gameObject.transform.position = originalPosition;

    }

    private IEnumerator refill()
    {
        yield return new WaitForSeconds(_refillCd); // timer
        this._currentCapacity = MAX_CAPACITY;
        this._isReloading = false;

        foreach(var obj in this._potionCharges)
        {
            obj.SetActive(true);
        }
    }
}
