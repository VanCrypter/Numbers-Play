using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class RadialBarView : MonoBehaviour
    {
        [SerializeField] private Image _radialBar;

        public void UpdateBar(float progress)
        {
            _radialBar.fillAmount = progress;
        }
    }
}