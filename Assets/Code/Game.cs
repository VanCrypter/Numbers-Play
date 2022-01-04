using System;
using System.Collections.Generic;
using Code.Blocks;
using Code.GameUIView;
using UnityEngine;
using Random = System.Random;

namespace Code
{
    public class Game : MonoBehaviour
    {
        [SerializeField]private GameObject _mainMenu;
        [SerializeField] private GameObject _timersGameObject;
        [SerializeField] private PlayingArea _playingArea;
        [SerializeField] private NumberTimer _localTimer;
        [SerializeField] private NumberTimer _globalTimer;
        [SerializeField]private TargetView _targetView;
        [SerializeField] private PoolBlocks _pool;
        [SerializeField] [Range(1,10)] private int _minTargetNumber;
        [SerializeField,Range(5,50)] private int _maxTargetNumber;
        private BlocksService _blocksService;
        private int _targetNumber=0;
        private int _score = 0;
        public BlocksService GetBlocksService => _blocksService;

        private void Awake()
        {
            _blocksService = new BlocksService(_pool, _playingArea);
        }

        public void StartGame()
        {
            _playingArea.ConstructPlayingArea();
            _timersGameObject.SetActive(true);
            _blocksService.ChangedSelected += OnChangeSelected;
            _globalTimer.StartTimer();
            NextNumber();
            _localTimer.TimerEnded += TimerWasExpired;
            _globalTimer.TimerEnded += GlobalTimerWasExpired;
        }

        private void GlobalTimerWasExpired()
        {
            Debug.Log($"timer game ended");
            GameOver();
        }

        private void GameOver()
        {
            _localTimer.StopTimer();
            _globalTimer.StopTimer();
            //***************
            _playingArea.DestructionPlayingArea();
            //**************
            _blocksService.ChangedSelected -= OnChangeSelected;
            _localTimer.TimerEnded -= TimerWasExpired;
            _globalTimer.TimerEnded -= GlobalTimerWasExpired;
            _mainMenu.SetActive(true);
            _timersGameObject.SetActive(false);
            Debug.Log($"Game over");
        }

        private void CalculateSelectedBlocks()
        {
            if (_blocksService.SumBlocks == _targetNumber)
            {
                _score++;
                _blocksService.RemoveSelectedBlocks();
                NextNumber();
            }
        }

        private void OnChangeSelected()
        {
            CalculateSelectedBlocks();
        }

        private void NextNumber()
        {
            _blocksService.ResetActiveNumbers();
            _targetNumber = UnityEngine.Random.Range(_minTargetNumber, _maxTargetNumber);
            _targetView.Display(_targetNumber);
            _localTimer.StartTimer();
        }
        private void TimerWasExpired() =>
            NextNumber();
    }
}