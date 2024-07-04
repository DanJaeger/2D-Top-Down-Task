using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header(header:"Text Options: ")]
        [SerializeField] private string _input;
        private Text _textHolder;
        [SerializeField] private Color _textColor;
        [SerializeField] private Font _textFont;

        [Header(header: "Time Parameters: ")]
        [SerializeField] private float _delay;

        [Header(header: "Sound: ")]
        [SerializeField] private AudioClip _sound;

        private void Awake()
        {
            _textHolder = GetComponent<Text>();
            _textHolder.text = "";
        }
        private void Start()
        {
            StartCoroutine(WriteText(_input, _textHolder, _textColor, _textFont, _delay, _sound));
        }
    }
}