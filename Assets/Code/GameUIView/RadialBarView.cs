using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class RadialBarView : MonoBehaviour
    {
        private Image _radialBar;

        private void Awake()
        {
            _radialBar = GetComponent<Image>();
        }

        public void UpdateBar(float progress)
        {
            _radialBar.fillAmount = progress;
        }
    }
}