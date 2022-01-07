using UnityEngine;
using TMPro;
namespace Code
{
    public abstract class View : MonoBehaviour
    {
        protected TMP_Text _text;
        protected void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        public abstract void Display(int number);
    }
}