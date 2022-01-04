using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class NumberTimer : MonoBehaviour
    {
        [SerializeField] private RadialBarView _radialBarView;
        [SerializeField] private float _duration = 10f;
        private Coroutine _timer;

        public Action TimerEnded;

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
            StopAllCoroutines();
            _timer = StartCoroutine(Timer());
        }

        public void StopTimer()
        {
            if (_timer != null)
                StopCoroutine(_timer);
        }

        private IEnumerator Timer()
        {
            Progress = 1f;
            var timer = _duration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                Progress -= Time.deltaTime / _duration;
                _radialBarView.UpdateBar(Progress);
                yield return null;
            }

            TimerEnded?.Invoke();
        }

        private void ResetTimer()
        {
            Progress = 1f;
            _radialBarView.UpdateBar(Progress);
            StartTimer();
        }
    }
}