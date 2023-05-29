using System;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.Gameplay
{
    public class PlayerChoiceButtons : MonoBehaviour
    {
        public event Action OnTakeClick;
        public event Action OnStandClick;
        public event Action OnSplitClick;
        public event Action OnDoubleClick;
        public event Action OnPlayClick;
        public event Action OnClearClick;
        
        [SerializeField] private Button _takeButton;
        [SerializeField] private Button _standButton;
        [SerializeField] private Button _splitButton;
        [SerializeField] private Button _doubleButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _clearButton;

        private ISoundService _soundService;

        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
            SubscribeChoiceButtons();
        }

        public void EnableClearAndPlay() =>
            _clearButton.interactable = _playButton.interactable = true;

        public void DisableClearAndPlay() =>
            _clearButton.interactable = _playButton.interactable = false;

        public void EnableTurn(bool isSplit, bool isDouble)
        {
            _takeButton.interactable = _standButton.interactable = true;
            _doubleButton.interactable = isDouble;
            _splitButton.interactable = isSplit;
        }

        public void DisableAll() =>
            _takeButton.interactable = _standButton.interactable = _splitButton.interactable =
                _doubleButton.interactable = _clearButton.interactable = false;

        private void SubscribeChoiceButtons()
        {
            _takeButton.onClick.AddListener(SendTakeButtonClick);
            _standButton.onClick.AddListener(SendStandButtonClick);
            _splitButton.onClick.AddListener(SendSplitButtonClick);
            _doubleButton.onClick.AddListener(SendDoubleButtonClick);
            _playButton.onClick.AddListener(SendPlayButtonClick);
            _clearButton.onClick.AddListener(SendClearButtonClick);
        }

        private void SendTakeButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            DisableAll();
            OnTakeClick?.Invoke();
        }

        private void SendStandButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            DisableAll();
            OnStandClick?.Invoke();
        }

        private void SendSplitButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            DisableAll();
            OnSplitClick?.Invoke();
        }

        private void SendDoubleButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            DisableAll();
            OnDoubleClick?.Invoke();
        }

        private void SendPlayButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnPlayClick?.Invoke();
        }

        private void SendClearButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnClearClick?.Invoke();
        }

        private void OnDestroy()
        {
            _takeButton.onClick.RemoveListener(SendTakeButtonClick);
            _standButton.onClick.RemoveListener(SendStandButtonClick);
            _splitButton.onClick.RemoveListener(SendSplitButtonClick);
            _doubleButton.onClick.RemoveListener(SendDoubleButtonClick);
            _playButton.onClick.RemoveListener(SendPlayButtonClick);
            _clearButton.onClick.RemoveListener(SendClearButtonClick);
        }
    }
}