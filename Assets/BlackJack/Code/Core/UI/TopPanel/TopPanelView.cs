using System;
using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.TopPanel
{
    public class TopPanelView : MonoBehaviour
    {
        public event Action OnExitClick;
        
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _settingsButton;

        private SettingsView _settingsPanel;
        private ISoundService _soundService;

        public void Construct(ISoundService soundService, SettingsView settingsPanel)
        {
            _soundService = soundService;
            _settingsPanel = settingsPanel;
        }

        private void Start()
        {
            _settingsButton.onClick.AddListener(SwitchSettingsPanel);
            _exitButton.onClick.AddListener(SendExitButtonClick);
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(SwitchSettingsPanel);
            _exitButton.onClick.RemoveListener(SendExitButtonClick);
        }

        private void SwitchSettingsPanel()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            _settingsPanel.gameObject.SetActive(!_settingsPanel.gameObject.activeSelf);
        }

        private void SendExitButtonClick()
        {
            _soundService.PlayEffectSound(SoundId.Click);
            OnExitClick?.Invoke();
        }
    }
}