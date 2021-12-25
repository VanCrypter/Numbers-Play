using TMPro;
using UnityEngine;

namespace Code
{
    public class NumberView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberText;

        public void DisplayNumber(int number)
        {
            _numberText.text = number.ToString();
        }
    }
}