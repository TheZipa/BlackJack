using System;
using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStatisticButtonClick;
        public event Action OnPlayButtonClick;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statisticButton;
        [SerializeField] private Button _settingsButton;

        private SettingsView _settingsView;
        private ISoundService _soundService;
        
        public void Construct(ISoundService soundService) => _soundService = soundService;
        
        public void SetSettingsPanel(SettingsView settingsPanel) =>
            _settingsView = settingsPanel;

        private void Awake()
        {
            _playButton.onClick.AddListener(SendPlayButtonClick);
            _settingsButton.onClick.AddListener(SwitchSettingsPanel);
            _statisticButton.onClick.AddListener(SendStatisticButtonClick);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(SendPlayButtonClick);
            _settingsButton.onClick.RemoveListener(SwitchSettingsPanel);
            _statisticButton.onClick.RemoveListener(SendStatisticButtonClick);
        }

        private void SwitchSettingsPanel()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _settingsView.gameObject.SetActive(!_settingsView.gameObject.activeSelf);
        }

        private void SendStatisticButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnStatisticButtonClick?.Invoke();
        }

        private void SendPlayButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnPlayButtonClick?.Invoke();
        }
    }
}
