using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace Code.Blocks
{
    public class Block : MonoBehaviour, IPointerClickHandler
    {
        public int Number;/* { get; private set; }*/
        public int x, y;
        private BlockView _blockView;
        private PlayingField _playingField;
        private readonly Vector2 _normalSize = Vector2.one;
        private readonly Vector2 _selectedSize = new Vector2(0.75f, 0.75f);
        private Vector3 targetPosition;
        private float _speedMove;
        private bool _selected;

        public event Action<Block> Selected;
        public event Action<Block> Deselected;

        private void Awake() =>
            _blockView = GetComponent<BlockView>();
        private void Start()
        {
            DOTween.Init();
        }
        public void Initialize(PlayingField playingField, Transform parent, Vector3 position, int coordX, int coordY)
        {
            _playingField = playingField;
            transform.SetParent(parent);
            transform.position = position;
            targetPosition = transform.localPosition;
            x = coordX;
            y = coordY;
            Number = Random.Range(1, 5);            
            _blockView.SetNumber(Number);
            _speedMove = 1f;
            transform.localScale = _normalSize;
            gameObject.SetActive(true);
            _selected = false;

        }

        public void ResetBlock() 
        {
            _selected = false;
            transform.localScale = _normalSize;
        
        }

        private void Select()
        {
            _selected = true;
            // _playingField.AddToSelected(this);
            transform.localScale = _selectedSize;
            Selected?.Invoke(this);
        }

        private void Deselect()
        {
            _selected = false;
            //_playingField.RemoveFromSelected(this);
            transform.localScale = _normalSize;
            Deselected?.Invoke(this);
        }
            

        public void Move() 
        {
            targetPosition += Vector3.down;
            y--;
            transform.DOMove(targetPosition, _speedMove);         
        }

           
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selected)
                Deselect();
            else
                Select();
        }
    }
}