using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Code.Blocks;
using UnityEngine;

namespace Code
{
    public class PlayingField : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private PoolBlocks _pool;
        [SerializeField] private Transform _parent;
        private float _sizeBlock;
        private Vector2 _bufferPosition;

        private Block[,] _field;
        private Dictionary<Vector2Int, Vector3> _positions = new Dictionary<Vector2Int, Vector3>();
        [SerializeField] private List<Block> _selectedBlocks = new List<Block>();
        [SerializeField] private List<Block> _inGameBlocks = new List<Block>();

        public event Action SelectedChanged;
        private int sumSelectedBlocks;

        public int SumSelectedBlocks
        {
            get => CalculateSelectedBlocks();

        }


        private void Awake()
        {
            InitializeGrid();
        }

        public void CreatePlayingField()
        {
            GeneratePlayingField();

        }

        private void AddToSelected(Block block)
        {
            if (block != null)
                _selectedBlocks.Add(block);

            SelectedChanged?.Invoke();
        }
        public void ResetSelectedBlocks()
        {
            foreach (var block in _selectedBlocks)
            {
                block.ResetBlock();
            }
            _selectedBlocks.Clear();
        }
        public void ResetPlayingField()
        {          

            foreach (var block in _inGameBlocks)
            {
                
                _pool.ReturnBlock(block);
                block.Selected -= OnSelectedBlock;
                block.Deselected -= OnDeselectedBlock;
            }

            _inGameBlocks.Clear();
            _positions.Clear();

        }

        public void DeleteSelectedBlocks()
        {
            foreach (var block in _selectedBlocks)
            {
                if (_inGameBlocks.Contains(block))
                _inGameBlocks.Remove(block);
                                
                _pool.ReturnBlock(block);
                block.Selected -= OnSelectedBlock;
                block.Deselected -= OnDeselectedBlock;
            }

            ShiftBloks();

            _selectedBlocks.Clear();

           // AddNewBlocks();

        }
        private void ShiftBloks()
        {

            for (int i = 0; i < _selectedBlocks.Count; i++)
            {
                ShiftBloksUpperThan(_selectedBlocks[i]);
                AddNewBlock(_selectedBlocks[i].x);
            }

        }

        private void ShiftBloksUpperThan(Block block)
        {
         
            for (int i = block.y+1; i < _gridSize.y; i++)
            {
                _field[block.x, i].Move();
                _field[block.x, i - 1] = _field[block.x, i];
            }

  

        }

        private void AddNewBlock(int coordX)
        {           
                 var block = _pool.GetBlock();
                block.Initialize(this, _parent, _positions[new Vector2Int(coordX,_gridSize.y-1)], coordX, _gridSize.y-1);
                _field[coordX, _gridSize.y-1] = block;
                _inGameBlocks.Add(block);
                block.Selected += OnSelectedBlock;
                block.Deselected += OnDeselectedBlock;
        }

        private int CalculateSelectedBlocks()
        {
            int count = 0;
            foreach (var block in _selectedBlocks)
                count += block.Number;

            return count;
        }             
        private void OnDeselectedBlock(Block block)
        {
            if (_selectedBlocks.Contains(block))
            {
                _selectedBlocks.Remove(block);
                SelectedChanged?.Invoke();
            }
        }
        private void OnSelectedBlock(Block block)
        {
            AddToSelected(block);        
        }

        private void GeneratePlayingField()
        {
            _bufferPosition = _startPosition;
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    _bufferPosition = new Vector2(_bufferPosition.x, _bufferPosition.y + _sizeBlock);
                    var block = _pool.GetBlock();
                    block.Initialize(this, _parent, _bufferPosition, x, y);
                    _field[x, y] = block;
                    _inGameBlocks.Add(block);
                    block.Selected += OnSelectedBlock;
                    block.Deselected += OnDeselectedBlock;
                    _positions.Add(new Vector2Int(x, y), _bufferPosition);

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

        private void DebugMatrix3x4(string startMessage)
        {
            Debug.Log($"{startMessage} \n" +
                        $"___________\n" +
                        $"|{_field[0, 3]?.Number} | {_field[1, 3]?.Number} | {_field[2, 3]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 2]?.Number} | {_field[1, 2]?.Number} | {_field[2, 2]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 1]?.Number} | {_field[1, 1]?.Number} | {_field[2, 1]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 0]?.Number} | {_field[1, 0]?.Number} | {_field[2, 0]?.Number}|\n" +
                        $"___________");
        }

    }
}