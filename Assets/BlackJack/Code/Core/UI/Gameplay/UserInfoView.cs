using BlackJack.Code.Core.Players;
using TMPro;
using UnityEngine;

namespace BlackJack.Code.Core.UI.Gameplay
{
    public class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _betText;
        [SerializeField] private TextMeshProUGUI _dealerText;
        [SerializeField] private TextMeshProUGUI _playerText;

        public void UpdateBalance(int balance) => _balanceText.text = balance.ToString();
        
        public void UpdateBet(int bet) => _betText.text = bet.ToString();

        public void UpdateDealerText(int cardsValue) => _dealerText.text = $"Dealer: {cardsValue}";
        
        public void UpdatePlayerText(int cardsValue) => _playerText.text = $"Player: {cardsValue}";
        
        public void ResetPlayersText()
        {
            UpdateDealerText(0);
            UpdatePlayerText(0);
        }
    }
}