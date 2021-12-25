using System;
using Code.Blocks;
using UnityEngine;

namespace Code
{
    public class PlayingArea:MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Block _prefabBlock;
        [SerializeField] private PoolBlocks _pool;
        [SerializeField] private Transform _parent;
        private float _sizeBlock;
        private Vector2 _bufferPosition;
        private Block[,] _field;


        public void ConstructPlayingArea()
        {
            GeneratePlayingArea();
        }

        private void GeneratePlayingArea()
        {
            InitializeGrid();
            
            _bufferPosition = _startPosition;
            for (int i = 0; i < _gridSize.y; i++)
            {
                for (int j = 0; j < _gridSize.x; j++)
                {
                    _bufferPosition = new Vector2(_bufferPosition.x + _sizeBlock, _bufferPosition.y);
                    var block = _pool.GetBlock();
                    block.transform.SetParent(_parent);
                    block.transform.localPosition = _bufferPosition;
                    block.gameObject.SetActive(true);
                    _field[j, i] = block;
                }

                _bufferPosition = new Vector2(_startPosition.x, _bufferPosition.y + _sizeBlock);
            }
        }

        private void InitializeGrid()
        {
            if (GridIsZero()) 
                _gridSize = new Vector2Int(5, 10);

            _field = new Block[_gridSize.x, _gridSize.y];
            _sizeBlock = _prefabBlock.Size;
        }

        private bool GridIsZero() =>
            _gridSize.x <= 0 || _gridSize.y <= 0;
    }
}