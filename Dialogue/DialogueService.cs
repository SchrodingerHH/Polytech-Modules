using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ink.Runtime;
using Zenject;
using Player;

namespace Dialogue
{
    public class DialogueService : MonoBehaviour
    {
        public bool isDialoguePlaying { get; private set; }
        public bool hasChoices { get; private set; }
        private int lastSelectedChoice;
        
        private Story _inkStory;

        public event Action<string> onDialogueLineUpdate;
        public event Action<Dictionary<int, string>> onChoicesUpdate;

        private PlayerInputHandler _playerInputHandler;
        
        [Inject]
        private void Construct(PlayerInputHandler inputHandler)
        {
            _playerInputHandler = inputHandler;
        }

        
        public void ImportDialogue(TextAsset inkAsset)
        {
            _inkStory = new Story(inkAsset.text);
        }

        public void EnterDialogue()
        {
            isDialoguePlaying = true;
            
            ContinueDialogue();
        }

        public void ExitDialogue()
        {
            isDialoguePlaying = false;
        }

        public void ContinueDialogue()
        {
            if (hasChoices)
            {
                return;
            }

            if (_inkStory.canContinue)
            {
                _inkStory.Continue();
                UpdateChoices();
                onDialogueLineUpdate?.Invoke(_inkStory.currentText);
            }
            else
            {
                ExitDialogue();
            }
        }

        public void UpdateChoices()
        {
            List<Choice> choices = _inkStory.currentChoices;
            Dictionary<int, string> choicesDict = choices.ToDictionary(choice => choice.index, choice => choice.text);
            onChoicesUpdate?.Invoke(choicesDict);
            if (choices.Count != 0)
            {
            }
        }

        public void MakeChoice(int choiceIndex)
        {
            lastSelectedChoice = choiceIndex;
            _inkStory.ChooseChoiceIndex(choiceIndex);
            ContinueDialogue();
        }
    }
}