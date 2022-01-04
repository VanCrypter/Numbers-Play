using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Code.Blocks
{
    public class Block : MonoBehaviour, IPointerClickHandler
    {
        public int Number { get; private set; }

        private BlocksService _blocksService;
        private BlockView _blockView;
        private readonly Vector2 _normalSize = Vector2.one;
        private readonly Vector2 _selectedSize = new Vector2(0.75f, 0.75f);
        public Vector3 targetPosition;
        private float _speedMove;

        private bool Selected { get; set; }

        private void Awake() =>
            _blockView = GetComponent<BlockView>();

        public void Initialize(BlocksService service)
        {
            _blocksService = service;
            Number = Random.Range(1, 5);
            name = "randomName" + Number;
            _blockView.SetNumber(Number);
            _speedMove = 3f;
        }

        public void Select()
        {
            Selected = true;
            _blocksService.AddToSelected(this);
            transform.localScale = _selectedSize;
        }

        public void Deselect()
        {
            Selected = false;
            transform.localScale = _normalSize;
            _blocksService.RevertFromSelected(this);
        }

        public void ResetSelectedBlock()
        {
            Selected = false;
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
                transform.position += Vector3.down *_speedMove * Time.deltaTime ;
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