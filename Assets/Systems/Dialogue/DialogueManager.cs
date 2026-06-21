using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string textRaw;
    public List<string> textList;

    void Awake()
    {
        ProcessDialogueText();
    }

    public bool DialogueToTextbox()
    {


        return true; //Whether it's the final line in the dialogue or not
    }

    public void ProcessDialogueText()
    {
        int _characterLength = 100;
        int _arraySize = Mathf.CeilToInt(textRaw.Length / _characterLength);
        int _startIndex = 0;
        string[] _textToRead = new string[_arraySize];

        for (int i = 0; i < _arraySize; i++)
        {
            _textToRead[i] = textRaw.Substring(_startIndex, _characterLength);
            _startIndex = _startIndex + _characterLength;
            textList.Add(_textToRead[i]);
        }
    }
}
