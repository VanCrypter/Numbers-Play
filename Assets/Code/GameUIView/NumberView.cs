using TMPro;
using UnityEngine;

namespace Code
{
    public class NumberView : View
    {

        public override void Display(int number)
        {
            _text.text = number.ToString();
        }


    }
}