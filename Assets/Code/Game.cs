using System.Collections.Generic;
using Code.Blocks;
using UnityEditor.Purchasing;
using UnityEngine;

namespace Code
{
    public class Game :MonoBehaviour
    {
        [SerializeField]private PlayingArea  _playingArea;
        [SerializeField] private NumberTimer _timer;
        private List<Block> _selectedBlocks;
        public void StartGame()
        {
            _playingArea.ConstructPlayingArea();
            _timer.StartTimer();
        }

        public void AddToSelected(Block block)
        {
            if (block !=null)
                _selectedBlocks.Add(block);
            
        }

    }
}