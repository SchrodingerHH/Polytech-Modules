using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Minigames.LetterCodeGame
{
    [CreateAssetMenu(fileName = "LetterBlockSO", menuName = "Minigames/LetterBlockSO", order = 0)]
    public class LetterBlockSO : ScriptableObject
    {
        [SerializeField]
        public List<LetterSequenceElement> sequenceElements;
    }

    [Serializable]
    public class LetterSequenceElement
    {
        public string lettersString;
        
        public bool isFixed;

#if UNITY_EDITOR
        [OnInspectorGUI] //Odin Inspector dependency
        public void OnInspectorGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;
            style.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Text preview");
            EditorGUILayout.LabelField(lettersString, style);
            EditorGUILayout.EndHorizontal();
        }
#endif
    }
}