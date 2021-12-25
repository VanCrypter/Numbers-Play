using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Blocks
{
    public class BlockView :MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _colorSprite;
        [SerializeField] private TextMeshPro _textNumber;

        public void SetNumber(int number)
        {
            if (number > 0)
                _textNumber.text = number.ToString();
        }

        public void SetColor(Sprite sprite) => _colorSprite.sprite = sprite;
    }
}