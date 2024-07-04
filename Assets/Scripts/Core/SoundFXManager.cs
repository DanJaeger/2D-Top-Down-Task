using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance { get; private set; }

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _buttonAudioClip;
    [SerializeField] private AudioClip _interactionAudioClip;
    [SerializeField] private AudioClip _footStepAudioClip;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Could NOT found an AudioSource Component on " + this.name);
        }
    }
    private void Start()
    {
        if (_audioSource != null)
        {
            _audioSource.loop = false;
        }
    }
    public void PlaySound(AudioClip audio)
    {
        _audioSource.PlayOneShot(audio);
    }
    public void PlayButtonSound()
    {
        _audioSource.PlayOneShot(_buttonAudioClip);
    }
    public void PlayInteractionSound()
    {
        _audioSource.PlayOneShot(_interactionAudioClip);
    }
    public void PlayStepSound()
    {
        _audioSource.PlayOneShot(_footStepAudioClip);
    }
}
