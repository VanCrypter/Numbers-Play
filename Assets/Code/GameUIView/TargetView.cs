using TMPro;
using UnityEngine;

namespace Code.GameUIView
{
    public class TargetView : View
    {          
        public override void Display(int number)
        {
            _text.text = number.ToString();
        }
    }
}