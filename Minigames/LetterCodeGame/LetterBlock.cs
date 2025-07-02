using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Minigames.LetterCodeGame
{
    public class LetterBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public string lettersString;
        
        [SerializeField] 
        public TextMeshProUGUI letterBlockText; 
        private GraphicRaycaster _graphicRaycaster;
        private Canvas _canvas;
        
        private Transform _parentAfterDrag;
        private Transform _parentBeforeDrag;


        private void Awake()
        {
            _canvas = FindFirstObjectByType<LetterCodeUI>().GetComponent<Canvas>(); //TODO: Исправить костыль
            
            //_canvas = FindFirstObjectByType<Canvas>(); 
            _graphicRaycaster = _canvas.gameObject.GetComponent<GraphicRaycaster>();
        }

        public void Setup(string text)
        {
            lettersString = text;
            letterBlockText.text = text;
        }

        public void Lift()
        {
            _parentBeforeDrag = transform.parent;
            _parentAfterDrag = _canvas.transform;
            //_parentAfterDrag = transform.parent;
            transform.SetParent(_canvas.transform);
            transform.SetAsLastSibling();
        }

        public void Drop(Transform socketTransform)
        {
            transform.position = socketTransform.position;
            transform.SetParent(socketTransform);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 correctedPosition = eventData.pointerCurrentRaycast.worldPosition; // _canvas.scaleFactor
            transform.position = correctedPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (SocketRaycast(eventData, out var letterSocket))
            {
                Debug.Log(letterSocket);
                if (letterSocket.state == LetterSocketState.Fixed)
                {
                    eventData.pointerDrag = null;
                    return;
                }
                
                letterSocket.RemoveBlock();
            }
            Lift();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (SocketRaycast(eventData, out var letterSocket))
            {
                Debug.Log(letterSocket);
                if (letterSocket.state == LetterSocketState.Free)
                {
                    letterSocket.InsertBlock(this);
                    Drop(letterSocket.gameObject.transform);
                    return;
                }
                /*switch (letterSocket.state)
                {
                    case LetterSocketState.Free:
                        letterSocket.InsertBlock(this);
                        Drop(letterSocket.gameObject.transform);
                        break;
                    case LetterSocketState.Inserted:
                        var socket = _parentBeforeDrag.GetComponent<LetterSocket>();
                        socket.InsertBlock(this);
                        Drop(_parentBeforeDrag);
                        break;
                    case LetterSocketState.Fixed:
                        break;
                        //return;
                }*/
            }
            else
            {
                var socket = _parentBeforeDrag.GetComponent<LetterSocket>();
                socket.InsertBlock(this);
                Drop(_parentBeforeDrag);
            }
        }

        private bool SocketRaycast(PointerEventData eventData, out LetterSocket letterSocket)
        {
            var results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(eventData, results);

            LetterSocket socket = null;
            results.FirstOrDefault(x => x.gameObject.TryGetComponent<LetterSocket>(out socket));
            letterSocket = socket;
            return socket != null;
        }
    }
}