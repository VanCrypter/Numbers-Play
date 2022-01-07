using System;
using System.Linq;
using System.Collections.Generic;
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

        public Action SelectedChanged;
        public int SumSelectedBlocks { get; set; }

        private void Awake()
        {
            InitializeGrid();
        }

        public void CreatePlayingField()
        {
            GeneratePlayingField();
            Debug.Log($"original matrix= \n" +
                          $"___________\n" +
                          $"|{_field[0, 3].Number} | {_field[1, 3].Number} | {_field[2, 3].Number}|\n" +
                          $"___________\n"+
                          $"|{_field[0, 2].Number} | {_field[1, 2].Number} | {_field[2, 2].Number}|\n" +
                          $"___________\n" +
                          $"|{_field[0, 1].Number} | {_field[1, 1].Number} | {_field[2, 1].Number}|\n" +
                          $"___________\n" +
                          $"|{_field[0, 0].Number} | {_field[1, 0].Number} | {_field[2, 0].Number}|\n" +
                          $"___________");

        }

        public void AddSelected(Block block)
        {
            SumSelectedBlocks += block.Number;
            SelectedChanged?.Invoke();
        }

        public void RevertSelected(Block block)
        {
            SumSelectedBlocks -= block.Number;

            if (SumSelectedBlocks < 0)
                SumSelectedBlocks = 0;

            SelectedChanged?.Invoke();
        }

        internal void ShowLog(int targetNumber)
        {
            Debug.Log($"target num = {targetNumber}");
        }

        public void RemoveSelected()
        {
            //foreach (var block in _field)
            //{
            //    if (block != null && block.Selected)
            //    {
            //        _pool.ReturnBlock(block);
            //        ShiftBlocksUpperThan(block);
            //        //_field[block.x, block.y] = null;
            //    }
            //}

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {                  
                    if (_field[x, y] != null && _field[x, y].Selected)
                    {
                        _pool.ReturnBlock(_field[x, y]);
                        _field[x, y] = null;
                        Debug.Log($"matrix after null but before ahifts=  \n" +
                        $"___________\n" +
                        $"|{_field[0, 3]?.Number} | {_field[1, 3]?.Number} | {_field[2, 3]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 2]?.Number} | {_field[1, 2]?.Number} | {_field[2, 2]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 1]?.Number} | {_field[1, 1]?.Number} | {_field[2, 1]?.Number}|\n" +
                        $"___________\n" +
                        $"|{_field[0, 0]?.Number} | {_field[1, 0]?.Number} | {_field[2, 0]?.Number}|\n" +
                        $"___________");
                        // ShiftColumnUpperThan(x, y);
                    }
                    //если поле == нулл, то сдвигаем все блоки выше текущего на 1 единицу

                    if (_field[x, y] == null && y < _gridSize.y)
                    {
                        if (y+1<_gridSize.y && _field[x, y + 1] != null)
                        {
                            _field[x, y + 1].Move();
                            _field[x, y] = _field[x, y + 1];
                            _field[x, y + 1] = null;
                        }
                    }

                }
            }

            Debug.Log($"matrix after shifts= \n" +
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

        //private void ShiftColumnUpperThan(int xPos,int yPos)
        //{
        //    for (int y = yPos; y < _gridSize.y-1; y++)
        //    {
        //        _field[xPos, y] = _field[xPos, y + 1];

        //        if (_field[xPos, y] != null)
        //            _field[xPos, y].Move();

        //        Debug.Log($"moved or not block {_field[xPos,y]}");
        //    }
        //}

        private void ShiftBlocksUpperThan(Block deletedBlock)
        {
            //_field[block.x, block.y] = _field[block.x, block.y + 1];
            for (int y = deletedBlock.y; y < _gridSize.y; y++)
            {
                if (y + 1 < _gridSize.y)
                {
                    var shiftedBlock = _field[deletedBlock.x, y + 1];
                    _field[deletedBlock.x, y] = shiftedBlock;
                    shiftedBlock.Move();
                }

            }
        }

        public void Hide()
        {
            foreach (var item in _field)
            {
                if (item != null)
                {
                    item.Deselect();
                    _pool.ReturnBlock(item);
                }
            }
        }
        public void DeselectAllBlocks()
        {
            foreach (var item in _field)
            {
                if (item != null)
                {
                    item.Deselect();
                }
            }
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
                    block.transform.SetParent(_parent);
                    block.transform.position = _bufferPosition;
                    block.gameObject.SetActive(true);
                    block.Initialize(this);
                    block.x = x;
                    block.y = y;
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

    }
}