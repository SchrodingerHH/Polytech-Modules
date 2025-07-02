using UnityEngine;

namespace Minigames.LetterCodeGame
{
    public enum LetterSocketState
    {
        Free,
        Inserted,
        Fixed
    }
    
    public class LetterSocket : MonoBehaviour
    {
        public LetterBlock letterBlock;
        public LetterSocketState state;

        public void InsertFixed(LetterBlock block)
        {
            state = LetterSocketState.Fixed;
            letterBlock = block;
        }

        public void InsertBlock(LetterBlock block)
        {
            state = LetterSocketState.Inserted;
            letterBlock = block;
        }

        public void RemoveBlock()
        {
            state = LetterSocketState.Free;
            letterBlock = null;
        }
    }
}