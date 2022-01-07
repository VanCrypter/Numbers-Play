using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

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

        public bool Selected { get;private set; }

        private void Awake() =>
            _blockView = GetComponent<BlockView>();

        public void Initialize(PlayingField playingField)
        {
            _playingField = playingField;
            Number = Random.Range(1, 5);
            _blockView.SetNumber(Number);
            _speedMove = 3f;
            targetPosition = transform.position;
           // Debug.Log($"target pos={targetPosition}");
        }

        public void Select()
        {
            Selected = true;
            _playingField.AddSelected(this);
            transform.localScale = _selectedSize;
        }

        public void Deselect()
        {
            Selected = false;
            _playingField.RevertSelected(this);
            transform.localScale = _normalSize;
        }

        public void Move()
        {
            targetPosition += Vector3.down;
        }

        private void Update()
        {
            if ((targetPosition - transform.position).magnitude > 0.001f && transform.position.y > targetPosition.y)
            {
                transform.position += Vector3.down * _speedMove * Time.deltaTime;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Selected)
                Deselect();
            else
                Select();
        }
    }
}