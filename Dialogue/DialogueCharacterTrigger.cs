using UnityEngine;
using UnityEngine.Serialization;
using Ink.Runtime;
using Zenject;
using Interactibles;
using Menu;

namespace Dialogue
{
    public class DialogueCharacterTrigger : MonoBehaviour, IInteractible
    {
        public TextAsset dialogueFile;

        private DialogueService _dialogueService;
        private DialogueUiController _dialogueUiController;
        
        [Inject]
        private void Construct(DialogueService service)
        {
            _dialogueService = service;
            
            //_dialogueUiController = FindFirstObjectByType<DialogueUiController>();
        }
        
        public void Interact()
        {
            _dialogueService.ImportDialogue(dialogueFile);
            _dialogueService.EnterDialogue();
            
            //_dialogueUiController.Open();
        }
    }
}