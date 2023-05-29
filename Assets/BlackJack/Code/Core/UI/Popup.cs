using System;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI
{
    public class Popup : MonoBehaviour
    {
        public event Action OnClosed;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private Button _backgroundCloseButton;
        [SerializeField] private Button _closeButton;

        private ISoundService _soundService;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            SetInteractableCloseButtons(false);
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _backgroundCloseButton.onClick.AddListener(Hide);
            _closeButton.onClick.AddListener(Hide);
        }

        public void Construct(ISoundService soundService) => _soundService = soundService;

        public void ShowSingleResult(HandResult handResult)
        {
            _winText.text = "You " + handResult.Result;
            TryAddWinSum(handResult.WinSum);
            StartShow();
        }

        public void ShowSplitResult(HandResult handResult1, HandResult handResult2)
        {
            _winText.text = "Hand 1 - " + handResult1.Result;
            TryAddWinSum(handResult1.WinSum);
            _winText.text += "\nHand 2 - " + handResult2.Result;
            TryAddWinSum(handResult2.WinSum);
            StartShow();
        }

        private void TryAddWinSum(int winHand1)
        {
            if (winHand1 > 0) _winText.text += " " + winHand1;
        }

        private void StartShow()
        {
            _soundService.PlayEffectSound(SoundId.Popup);
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, 0.7f).OnComplete(() =>
                SetInteractableCloseButtons(true));
        }

        private void Hide()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            SetInteractableCloseButtons(false);
            _canvasGroup.DOFade(0, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                OnClosed?.Invoke();
            });
        }

        private void SetInteractableCloseButtons(bool isActive) =>
            _backgroundCloseButton.interactable = _closeButton.interactable = isActive;

        private void OnDestroy()
        {
            _backgroundCloseButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}