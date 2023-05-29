using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.Resolution
{
    public class BackgroundImageResolution : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _iPadBackground;
        [SerializeField] private Sprite _iPhoneBackground;

        private void Start() => _backgroundImage.sprite = Screen.width > 1500 
            ? _iPadBackground 
            : _iPhoneBackground;
    }
}