﻿using System;
using System.Collections.Generic;
using Code.Blocks;
using Code.GameUIView;
using UnityEngine;

namespace Code
{
    public class Game : MonoBehaviour
    {

        [SerializeField] private GamePlayUI _gameUI;
        [SerializeField] private PlayingField _playingField;
        [SerializeField] private Timer _localTimer;
        [SerializeField] private Timer _mainTimer;
        [SerializeField] private View _targetView;
        [SerializeField] private View _scoreView;
        [SerializeField] [Range(1, 10)] private int _minTargetNumber;
        [SerializeField, Range(5, 50)] private int _maxTargetNumber;
        private int _targetNumber = 0;
        private int _score;

        public int TargetNumber 
        {
            get { return _targetNumber; }
            private set 
            {
                _targetNumber = value;
                _targetView.Display(_targetNumber);            
            }
        }
        public int Score
        {
            get { return _score; }
            private set
            {
                _score = value;
                _scoreView.Display(_score);            
            }
        }
        private void Awake()
        {
            _localTimer.TimerExpired += TimerWasExpired;
            _mainTimer.TimerExpired += MainTimerWasExpired;
            _playingField.SelectedChanged += OnSelectedChanged;
        }
        public void StartGame()
        {
            _playingField.CreatePlayingField();
            _gameUI.Show();
            _mainTimer.StartTimer();
            Score = 0;
            NextNumber();
            _playingField.ShowLog(TargetNumber);

        }

        private void MainTimerWasExpired()
        {
            GameOver();
        }

        private void GameOver()
        {
            _localTimer.StopTimer();
            _mainTimer.StopTimer();
            _playingField.Hide();
            _gameUI.Hide();

        }

        private void OnSelectedChanged()
        {
            CalculateSelectedBlocks();          
        }

        private void CalculateSelectedBlocks()
        {
            if (_playingField.SumSelectedBlocks == _targetNumber)
            {
                Score++;
                _playingField.RemoveSelected();
                NextNumber();              
            }
        }

        private void NextNumber()
        {
            _playingField.SumSelectedBlocks = 0;
            TargetNumber = UnityEngine.Random.Range(_minTargetNumber, _maxTargetNumber);          
            _playingField.DeselectAllBlocks();
            _localTimer.StartTimer();
        }
        private void TimerWasExpired() =>
            NextNumber();

        private void OnDestroy()
        {
            _localTimer.TimerExpired -= TimerWasExpired;
            _mainTimer.TimerExpired -= MainTimerWasExpired;
            _playingField.SelectedChanged -= OnSelectedChanged;
        }
    }
}