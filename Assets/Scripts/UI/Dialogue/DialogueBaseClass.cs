using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public abstract class DialogueBaseClass : MonoBehaviour
    {
        public bool IsFinished { get; private set; }
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, 
                                        AudioClip sound)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                SoundFXManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            IsFinished = true;
        }
    }
}