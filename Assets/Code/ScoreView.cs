using UnityEngine;
using TMPro;
namespace Code
{
    public class ScoreView : View
    {
        public override void Display(int number)
        {
            _text.text = number.ToString();
        }
    }
}