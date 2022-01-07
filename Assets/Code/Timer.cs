using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private RadialBarView _radialBarView;
        [SerializeField] private float _duration = 10f;
        private Coroutine _timer;
        private bool isPaused;
        public Action TimerExpired;


        /// <summary>
        /// Progress timer 0-1
        /// </summary>
        public float Progress { get; private set; }

        public void ChangeDuration(float duration)
        {
            if (duration > 0)
                _duration = duration;
        }

        public void StartTimer()
        {
            StopTimer();
            _timer = StartCoroutine(TimerCorutine());
        }

        public void Pause()
        {
            isPaused = true;
        }
        public void Resume()
        {
            isPaused = false;
        }
        public void StopTimer()
        {
            if (_timer != null)
                StopCoroutine(_timer);
        }

        private IEnumerator TimerCorutine()
        {
            Progress = 1f;
            var timer = _duration;
            while (timer > 0)
            {
                if (isPaused == false)
                {
                    timer -= Time.deltaTime;
                    Progress -= Time.deltaTime / _duration;
                    _radialBarView.UpdateBar(Progress);
                }
                yield return null;
            }

            TimerExpired?.Invoke();
        }

    }
}