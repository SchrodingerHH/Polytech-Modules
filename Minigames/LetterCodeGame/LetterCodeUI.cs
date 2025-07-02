using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Minigames.LetterCodeGame
{
    public class LetterCodeUI : MonoBehaviour
    {
        [SerializeField]
        private Button exitBtn;
        
        [SerializeField] 
        private Button checkCompletionBtn;

        [SerializeField]
        private GameObject modalWindow;

        [SerializeField] 
        private Image fadePanel;

        private LetterGameController _gameController;

        private void Awake()
        {
            fadePanel.gameObject.SetActive(true);
            fadePanel.DOFade(0, 0.2f);
            
            _gameController = FindFirstObjectByType<LetterGameController>();
            modalWindow.SetActive(false);
            checkCompletionBtn.onClick.AddListener(CheckCompletion);
            exitBtn.onClick.AddListener(_gameController.ExitGame);
        }

        private void CheckCompletion()
        {
            bool completionResult = false;
            _gameController.CheckCompletion(b => completionResult = b);

            TMP_Text text = modalWindow.GetComponentInChildren<TMP_Text>();
            modalWindow.SetActive(true);
            
            if (completionResult == true)
                text.SetText("<color=green>Зачёт");
            else
                text.SetText("<color=red>Неудача, попытайся снова");
            StartCoroutine(ModalWindowClose());
        }

        private IEnumerator ModalWindowClose()
        {
            yield return new WaitForSeconds(5f);
            modalWindow.SetActive(false);
        }
    }
}