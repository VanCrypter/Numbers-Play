using TMPro;
using UnityEngine;

namespace Code.GameUIView
{
    public class TargetView : MonoBehaviour
    {
        private TextMeshProUGUI _numberText;
        private void Awake()
        {
            _numberText = GetComponent<TextMeshProUGUI>();
        }

        public void Display(int number)
        {
            _numberText.text = number.ToString();
        }
    }
}