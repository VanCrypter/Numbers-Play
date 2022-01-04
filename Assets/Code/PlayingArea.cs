using Code.Blocks;
using UnityEngine;

namespace Code
{
    public class PlayingArea : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private PoolBlocks _pool;
        [SerializeField] private Transform _parent;
        private float _sizeBlock;
        private Vector2 _bufferPosition;
        private Block[,] _field;

        public void ConstructPlayingArea()
        {
            GeneratePlayingArea();
        }

        public void DestructionPlayingArea()
        {
            //destruct playing area
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    _field[x, y].Deselect();
                    _pool.ReturnBlock(_field[x,y]);
                }

                _bufferPosition = new Vector2(_bufferPosition.x + _sizeBlock, _startPosition.y);
            }
        }

        public void RemoveBlock(Block item)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (_field[x, y].Equals(item))
                    {
                        ShiftBlocksHigherThan(x, ++y);
                    }
                }
            }
        }

        private void GeneratePlayingArea()
        {
            InitializeGrid();

            _bufferPosition = _startPosition;
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    _bufferPosition = new Vector2(_bufferPosition.x, _bufferPosition.y + _sizeBlock);
                    var block = _pool.GetBlock();
                    block.transform.SetParent(_parent);
                    block.transform.position = _bufferPosition;
                    block.targetPosition = _bufferPosition;
                    block.gameObject.SetActive(true);

                    _field[x, y] = block;
                }

                _bufferPosition = new Vector2(_bufferPosition.x + _sizeBlock, _startPosition.y);
            }
        }

        private void InitializeGrid()
        {
            if (GridIsZero())
                _gridSize = new Vector2Int(5, 10);

            _field = new Block[_gridSize.x, _gridSize.y];
            _sizeBlock = 1f;
        }

        private bool GridIsZero() =>
            _gridSize.x <= 0 || _gridSize.y <= 0;

        private void ShiftBlocksHigherThan(int blockCol, int blockRow)
        {
            for (int y = blockRow; y < _gridSize.y; y++)
            {
                _field[blockCol, y].Move();
            }
        }
    }
}