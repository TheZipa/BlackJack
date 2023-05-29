using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class CardHandOutState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        
        private Hand _playerHand;
        private Hand _dealerHand;
        private CardDispenser _cardDispenser;

        public CardHandOutState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            SetEntityDependencies();
            _entityContainer.GetEntity<CardDeck>().Shuffle();
            StartHandOut();
        }

        public void Exit()
        {
        }

        private async void StartHandOut()
        {
            await _cardDispenser.DispenseTwoCardsForPlayer();
            await _cardDispenser.DispenseTwoCardsForDealer();
            UpdateHandTexts();
            FinishHandOut(_playerHand.IsBlackJack(), _dealerHand.GetFirstCardValue());
        }

        private void FinishHandOut(bool isPlayerBlackjack, int dealerFirstCardValue)
        {
            if (isPlayerBlackjack)
            {
                if(dealerFirstCardValue == 10 || dealerFirstCardValue == 11) _stateSwitcher.SwitchTo<DealerTurnState>();
                else _stateSwitcher.SwitchTo<CompareHandsState>();
            }
            else
            {
                _stateSwitcher.SwitchTo<PlayerChoiceState>();
            }
        }

        private void UpdateHandTexts()
        {
            UserInfoView userInfoView = _entityContainer.GetEntity<UserInfoView>();
            userInfoView.UpdateDealerText(_dealerHand.GetFirstCardValue());
            userInfoView.UpdatePlayerText(_playerHand.GetValue());
        }

        private void SetEntityDependencies()
        {
            PlayerHands playerHands = _entityContainer.GetEntity<PlayerHands>();
            _playerHand = playerHands.PlayerHand;
            _dealerHand = playerHands.DealerHand;
            playerHands.SetDefaultActiveHand();
            _cardDispenser = _entityContainer.GetEntity<CardDispenser>();
        }
    }
}