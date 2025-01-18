using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageShakeAndSound : MonoBehaviour, IPointerClickHandler
{
    public AudioClip soundEffect; // 音效文件
    public float shakeDuration = 0.5f; // 晃动持续时间
    public float shakeMagnitude = 10f; // 晃动幅度

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private AudioSource audioSource;

    private void Start()
    {
        // 获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;

        // 添加 AudioSource 组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 播放音效
        if (audioSource != null && soundEffect != null)
        {
            audioSource.Play();
        }

        // 开始晃动效果
        StopAllCoroutines(); // 防止多次点击导致晃动叠加
        StartCoroutine(Shake());
    }

    private System.Collections.IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // 随机生成晃动的偏移量
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

            rectTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 恢复到原始位置
        rectTransform.localPosition = originalPosition;
    }
}
