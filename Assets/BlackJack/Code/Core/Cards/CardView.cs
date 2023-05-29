using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BlackJack.Code.Core.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Sprite _valueSprite;
        private Sprite _shirt;
        
        public void Construct(Sprite valueSprite, Sprite shirt)
        {
            _spriteRenderer.sprite = _valueSprite = valueSprite;
            _shirt = shirt;
        }

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);

        public TweenerCore<Color, Color, ColorOptions> FadeColor(Color fadeColor, float speed) =>
            _spriteRenderer.DOColor(fadeColor, speed);

        public void SetLayer(int layer) => _spriteRenderer.sortingOrder = layer;

        public void SetValueSprite() => _spriteRenderer.sprite = _valueSprite;

        public void SetShirtSprite() => _spriteRenderer.sprite = _shirt;
    }
}