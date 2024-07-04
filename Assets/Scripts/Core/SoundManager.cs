using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _backgroundAudioClip;

    private void Awake()
    {
        instance = this;
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            if(_audioSource == null)
            {
                Debug.LogError("Could NOT found an AudioSource Component on " + this.name);
            }
        }
    }
    private void Start()
    {
        if (_audioSource != null)
        {
            _audioSource.loop = true;
            _audioSource.clip = _backgroundAudioClip;
            _audioSource.Play();
        }
    }
}
