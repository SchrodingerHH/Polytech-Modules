using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Minigames.LetterCodeGame
{
    public class LetterGameController : MinigameBehaviour
    {
        [SerializeField]
        private LetterBlockInitializer letterBlockInitializer;
        
        [SerializeField]
        public List<LetterSocket> requiredSockets;
        
        [SerializeField]
        public List<LetterSocket> spareSockets;
        
        [InlineEditor]
        [SerializeField] 
        private LetterBlockSO letterBlockSo;


        public override void Start()
        {
            base.Start();
            
            InitializeGame();
        }

        private void InitializeGame() => letterBlockInitializer.Initialize(requiredSockets, spareSockets, letterBlockSo);

        public void ExitGame()
        {
            MinigameState previousState = CurrentState;
            CurrentState = MinigameState.Fail;
            OnStateChanged?.Invoke(previousState, CurrentState);
        }
        
        public void CheckCompletion(Action<bool> callback) => StartCoroutine(CheckCoroutine(callback));
        
        private IEnumerator CheckCoroutine(Action<bool> callback)
        {
            bool result = letterBlockSo.sequenceElements
                         .Select((seq, i) => new {Expected = seq.lettersString, Actual = requiredSockets[i].letterBlock?.lettersString})
                         .All(pair => pair.Expected == pair.Actual);
            
            callback?.Invoke(result);
            
            yield return new WaitForSeconds(5f);

            if (result)
            {
                MinigameState previousState = CurrentState;
                CurrentState = MinigameState.Success;
                OnStateChanged?.Invoke(previousState, CurrentState);
            }
        }
    }

    [Serializable]
    public class LetterBlockInitializer
    {
        [SerializeField]
        private GameObject letterBlockPrefab;
        
        public async void Initialize(
            List<LetterSocket> requiredSockets, List<LetterSocket> spareSockets, 
            LetterBlockSO letterBlockSo)
        {
            List<LetterSequenceElement> fixedLetterBlocks = letterBlockSo.sequenceElements
                .Where(x => x.isFixed)
                .ToList();
            List<LetterSequenceElement> spareLetterBlocks = letterBlockSo.sequenceElements
                .Where(x => !x.isFixed)
                .ToList();
            
            List<LetterBlock> letterBlocks = new List<LetterBlock>();

            foreach (var fixedLetterBlock in fixedLetterBlocks)
            {
                LetterSocket freeSocket = requiredSockets[letterBlockSo.sequenceElements.IndexOf(fixedLetterBlock)];
                LetterBlock letterBlock = Object.Instantiate(letterBlockPrefab, freeSocket.transform).GetComponent<LetterBlock>();
                
                letterBlock.Setup(fixedLetterBlock.lettersString);
                letterBlock.Drop(freeSocket.transform);
                freeSocket.InsertFixed(letterBlock);
                letterBlocks.Add(letterBlock);
            }

            foreach (var spareLetterBlock in spareLetterBlocks)
            {
                LetterSocket freeSocket = spareSockets.First(x => x.state == LetterSocketState.Free);
                LetterBlock letterBlock = Object.Instantiate(letterBlockPrefab, freeSocket.transform).GetComponent<LetterBlock>();
                
                letterBlock.Setup(spareLetterBlock.lettersString);
                letterBlock.Drop(freeSocket.transform);
                freeSocket.InsertBlock(letterBlock);
                letterBlocks.Add(letterBlock);
            }

            //Size correction to smallest element
            await TextSizeCorrection(letterBlocks);
        }

        private async UniTask TextSizeCorrection(List<LetterBlock> letterBlocks)
        {
            await UniTask.WaitForEndOfFrame();
            
            float textSize = float.MaxValue;
            foreach (var letterBlock in letterBlocks)
            {
                if (letterBlock.letterBlockText.fontSize < textSize)
                {
                    textSize = letterBlock.letterBlockText.fontSize;
                }
            }

            foreach (var letterBlock in letterBlocks)
            {
                letterBlock.letterBlockText.enableAutoSizing = false;
                letterBlock.letterBlockText.fontSize = textSize;
            }
        }
    }
}
