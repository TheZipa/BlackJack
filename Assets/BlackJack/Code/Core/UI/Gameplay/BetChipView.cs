using System;
using BlackJack.Code.Data.StaticData.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackJack.Code.Core.UI.Gameplay
{
    public class BetChipView : MonoBehaviour
    {
        public event Action<int> OnChipClick;
        [SerializeField] private Button _chipButton;
        [SerializeField] private Image _chipImage;
        [SerializeField] private TextMeshProUGUI _valueText;
        private int _betValue;

        public void Construct(BetChipData betChipData)
        {
            _chipImage.sprite = betChipData.View;
            _betValue = betChipData.Value;
            _valueText.text = _betValue.ToString();
        }

        public void Enable() => _chipButton.interactable = true;

        public void Disable() => _chipButton.interactable = false;

        private void Start() => _chipButton.onClick.AddListener(SendChipButtonClick);
        
        private void SendChipButtonClick() => OnChipClick?.Invoke(_betValue);
        
        private void OnDestroy() => _chipButton.onClick.RemoveListener(SendChipButtonClick);
    }
}