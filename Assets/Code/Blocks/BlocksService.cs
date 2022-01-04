using System;
using System.Collections.Generic;

namespace Code.Blocks
{
    public class BlocksService
    {
        private List<Block> _selectedBlocks;
        private PlayingArea _playingArea;
        private PoolBlocks _pool;

        public Action ChangedSelected;
        public BlocksService(PoolBlocks pool,PlayingArea playingArea)
        {
            _pool = pool;
            _playingArea = playingArea;
            _selectedBlocks= new List<Block>();
        }

        public int SumBlocks => Recount();

        private int Recount()
        {
            if (_selectedBlocks.Count <= 0)
                return 0;

            int sum = 0;

            foreach (var block in _selectedBlocks)
            {
                sum += block.Number;
            }

            return sum;
        }

        public void RemoveSelectedBlocks()
        {
            foreach (var item in _selectedBlocks)
            {
                _playingArea.RemoveBlock(item);
                _pool.ReturnBlock(item);
            }

            _selectedBlocks.Clear();
        }

        public void AddToSelected(Block block)
        {
            if (block != null)
                _selectedBlocks.Add(block);

            ChangedSelected?.Invoke();
        }

        public void RevertFromSelected(Block block)
        {
            if (block != null)
                _selectedBlocks.Remove(block);
            
            ChangedSelected?.Invoke();
        }

        public void ResetActiveNumbers()
        {
            if (_selectedBlocks.Count <= 0)
                return;
            foreach (var block in _selectedBlocks)
                block.ResetSelectedBlock();
            
            _selectedBlocks.Clear();
        }
    }
}