using BlackJack.Code.Core.Cards;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BlackJack.Code.Services.CardMove
{
    public class CardMover : ICardMover
    {
        private readonly ISoundService _soundService;
        private readonly Vector2 _splitCardScale;
        private readonly Vector2 _defaultCardScale;
        private readonly Color _splitCardColor;
        private readonly float _animationSpeed;
        private readonly float _rotationSpeed;
        private readonly float _offset;

        public CardMover(IStaticData staticData, ISoundService soundService)
        {
            _soundService = soundService;
            _splitCardScale = staticData.BlackJackCardConfig.SplitCardScale;
            _splitCardColor = new Color(255, 255, 255, staticData.BlackJackCardConfig.SplitCardAlpha);
            _defaultCardScale = staticData.BlackJackPrefabs.CardViewPrefab.transform.localScale;
            _animationSpeed = staticData.BlackJackSettingsConfig.AnimationSpeed;
            _rotationSpeed = _animationSpeed * 0.5f;
            _offset = staticData.LocationData.CardOffset;
        }

        public UniTask MoveCardToSplit(CardView cardView, Vector2 position, int offsetIndex) =>
            MoveCardWithScaleAndFade(cardView, GetPositionWithOffset(position, offsetIndex), _splitCardScale, _splitCardColor);

        public UniTask MoveCardFromSplit(CardView cardView, Vector2 position, int offsetIndex) =>
            MoveCardWithScaleAndFade(cardView, GetPositionWithOffset(position, offsetIndex), _defaultCardScale, Color.white);

        public UniTask MoveCardWithOffset(CardView cardView, Vector2 position, int offsetIndex)
        {
            _soundService.PlayEffectSound(SoundId.CardHandOut);
            return cardView.transform.DOMove(GetPositionWithOffset(position, offsetIndex),
                _animationSpeed).SetEase(Ease.Linear).AsyncWaitForCompletion().AsUniTask();
        }

        public UniTask RotateCard(CardView cardView, bool isShirtUp)
        {
            _soundService.PlayEffectSound(SoundId.CardHandOut);
            return DOTween.Sequence()
                .SetEase(Ease.Linear)
                .Append(cardView.transform.DORotate(new Vector2(0, 90), _rotationSpeed))
                .AppendCallback(() =>
                {
                    if (isShirtUp) cardView.SetShirtSprite();
                    else cardView.SetValueSprite();
                })
                .Append(cardView.transform.DORotate(Vector3.zero, _rotationSpeed))
                .AsyncWaitForCompletion().AsUniTask();
        }

        private UniTask MoveCardWithScaleAndFade(CardView cardView, Vector2 position, Vector2 scale, Color fadeColor)
        {
            _soundService.PlayEffectSound(SoundId.CardHandOut);
            return DOTween.Sequence()
                .Join(cardView.transform.DOMove(position, _animationSpeed))
                .Join(cardView.transform.DOScale(scale, _animationSpeed))
                .Join(cardView.FadeColor(fadeColor, _animationSpeed))
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion().AsUniTask();
        }

        private Vector2 GetPositionWithOffset(Vector2 position, int offsetIndex) =>
            new Vector2(position.x + _offset * offsetIndex, position.y);
    }
}