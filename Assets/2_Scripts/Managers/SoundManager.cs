using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("BGM")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioClip deathClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBGMAudio();
    }

    void PlayBGMAudio()
    {
        if (bgmClip != null)
        {
            BGM.clip = bgmClip;
            BGM.Play();
        }
        else
        {
            Debug.LogError("bgmClip is null");
        }
    }

    public void PlaySFXAudio(string audioName)
    {
        audioName = audioName.ToLower();
        if (audioName == "coin")
        {
            SFX.clip = coinClip;
        }
        else if (audioName == "death")
        {
            SFX.clip = deathClip;
        }
        else
        {
            Debug.LogError($"Can't find {audioName} audio clip");
            return;
        }
        SFX.PlayOneShot(SFX.clip);
    }
}
