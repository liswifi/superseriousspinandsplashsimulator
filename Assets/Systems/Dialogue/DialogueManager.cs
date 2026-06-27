using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI guiText;
    public string textRaw;
    public List<string> textList;
    public int textIndex = 0;

    [SerializeField] GameObject textBox;
    [SerializeField] GameObject iconboxNext;
    [SerializeField] GameObject iconboxEnd;

    void Start()
    {
        ProcessDialogueText();
    }

    public bool DialogueToTextbox()
    {
        if (textIndex == textList.Count)
        {
            // ENTER GAMEPLAY FROM HERE!
            return true; //Final line in the dialogue, we should close the convo now
        }
        else
        {
            textBox.SetActive(true);
            iconboxNext.SetActive(true);
            iconboxEnd.SetActive(false);
            guiText.text = textList[textIndex];
            ++textIndex;
            if(textIndex == textList.Count)
            {
                iconboxNext.SetActive(false);
                iconboxEnd.SetActive(true);
                Debug.Log("End of conversation");
            }
            return false; //Not final line in the dialogue
        }
    }

    public void ProcessDialogueText() // Split string by whitespace instead of characters
    {
        //int _characterLength = 140;
        //int _arraySize = Mathf.FloorToInt((float)textRaw.Length / (float)_characterLength);
        //int _startIndex = 0;
        //string[] _textToRead = new string[_arraySize];

        //for (int i = 0; i < _arraySize; i++)
        //{
        //    textList.Add(textRaw.Substring(_startIndex, _characterLength)); 
        //    _startIndex = _startIndex + _characterLength;
        //}


        int _wordMax = 24;
        string[] _separatedDialogue = textRaw.Split(' ');
        List<string> _dialogueBlocks = new();
        string _dialogueBlockBuffer = "";
        int _currentWord = 0;
        int _currentBlock = 0;

        foreach (string word in _separatedDialogue)
        {
            _dialogueBlockBuffer = _dialogueBlockBuffer + " " + word;
            _currentWord++;
            if(_currentWord > _wordMax)
            {
                _dialogueBlocks.Add(_dialogueBlockBuffer);
                _dialogueBlockBuffer = "";
                _currentWord = 0;
                _currentBlock++;
            }
        }
        _dialogueBlocks.Add(_dialogueBlockBuffer);

        textList = _dialogueBlocks;

        //textList.Add(textRaw.Substring(_startIndex, textRaw.Length - (_arraySize) * _characterLength));
    }
}
