using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Walls_MiniGame
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _builtImage;
        [SerializeField] private Sprite _emptyImage;

        private Button _button;

        private void Start()
        {
            HideBrick();
        }

        public void ShowBrick()
        {
            _image.sprite = _builtImage;
        }

        public void HideBrick()
        {
            _image.sprite = _emptyImage;
        }
    }
}