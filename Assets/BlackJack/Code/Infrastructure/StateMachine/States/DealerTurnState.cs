using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.CardMove;
using BlackJack.Code.Services.EntityContainer;
using Cysharp.Threading.Tasks;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class DealerTurnState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly ICardMover _cardMover;

        private PlayerHands _playerHands;
        private CardDispenser _cardDispenser;
        private UserInfoView _userInfoView;

        public DealerTurnState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, ICardMover cardMover)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _cardMover = cardMover;
        }
        
        public async void Enter()
        {
            SetEntityDependencies();
            if (IsPlayerOverflow())
            {
                _stateSwitcher.SwitchTo<CompareHandsState>();
                return;
            }
            
            await ShowSecondDealerCard();
            
            if (IsDealerStand() || !_playerHands.IsPlayerSplit() && _playerHands.PlayerHand.IsBlackJack()) 
                _stateSwitcher.SwitchTo<CompareHandsState>();
            else 
                TakeDealerCardsUntilStand();
        }

        public void Exit()
        {
        }

        private async UniTask ShowSecondDealerCard()
        {
            await _cardMover.RotateCard(_playerHands.DealerHand.Cards[1].View, false);
            _userInfoView.UpdateDealerText(_playerHands.DealerHand.GetValue());
        }

        private async void TakeDealerCardsUntilStand()
        {
            while (!IsDealerStand()) await TakeCardForDealer();

            _stateSwitcher.SwitchTo<CompareHandsState>();
        }

        private async UniTask TakeCardForDealer()
        {
            await _cardDispenser.HandOutCardToHand(_playerHands.DealerHand);
            _userInfoView.UpdateDealerText(_playerHands.DealerHand.GetValue());
        }

        private bool IsDealerStand() => _playerHands.DealerHand.GetValue() >= 17;

        private bool IsPlayerOverflow() =>
            !_playerHands.IsPlayerSplit() 
                ? _playerHands.PlayerHand.GetValue() > 21
                : _playerHands.PlayerHand.GetValue() > 21 && _playerHands.SplitHand.GetValue() > 21;

        private void SetEntityDependencies()
        {
            _playerHands = _entityContainer.GetEntity<PlayerHands>();
            _cardDispenser = _entityContainer.GetEntity<CardDispenser>();
            _userInfoView = _entityContainer.GetEntity<UserInfoView>();
        }
    }
}