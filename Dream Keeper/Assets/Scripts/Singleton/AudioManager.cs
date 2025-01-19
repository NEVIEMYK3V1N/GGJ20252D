using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [Header("BottleShake")]
    // click bottle
    [SerializeField] public AudioSource _bottle_shake_audio_source;
    [SerializeField] public AudioClip _bottle_shake_audio_clip;

    [Header("Click")]
    // click button
    [SerializeField] public AudioSource _click_audio_source;
    [SerializeField] public AudioClip _click_audio_clip;

    [Header("Success")]
    // end screen
    [SerializeField] public AudioSource _success_audio_source;
    [SerializeField] public AudioClip _success_audio_clip;

    public void play_audio_bottle_shake()
    {
        _bottle_shake_audio_source.PlayOneShot(_bottle_shake_audio_clip);
    }

    public void play_audio_click()
    {
        _click_audio_source.PlayOneShot(_click_audio_clip);
    }

    public void play_audio_success()
    {
        _success_audio_source.PlayOneShot(_success_audio_clip);
    }
}
