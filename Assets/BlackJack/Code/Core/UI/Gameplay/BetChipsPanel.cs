using System;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Services.Sound;
using UnityEngine;

namespace BlackJack.Code.Core.UI.Gameplay
{
    public class BetChipsPanel : MonoBehaviour
    {
        public event Action<int> OnBetChipClick;

        private BetChipView[] _chips;
        private ISoundService _soundService;

        public void Construct(BetChipView[] chips, ISoundService soundService)
        {
            _chips = chips;
            _soundService = soundService;
            foreach (BetChipView betChip in _chips) 
                betChip.OnChipClick += SendBetChipClick;
        }

        public void Enable()
        {
            foreach (BetChipView betChip in _chips) 
                betChip.Enable();
        }

        public void Disable()
        {
            foreach (BetChipView betChip in _chips) 
                betChip.Disable();
        }

        private void SendBetChipClick(int chipValue)
        {
            _soundService.PlayEffectSound(SoundId.Chip);
            OnBetChipClick?.Invoke(chipValue);
        }

        private void OnDestroy()
        {
            foreach (BetChipView betChip in _chips) 
                betChip.OnChipClick -= SendBetChipClick;
        }
    }
}