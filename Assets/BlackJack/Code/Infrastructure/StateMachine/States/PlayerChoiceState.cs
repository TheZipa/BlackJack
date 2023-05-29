using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.UserBalance;
using Cysharp.Threading.Tasks;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class PlayerChoiceState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IUserBalance _userBalance;

        private CardDispenser _cardDispenser;
        private PlayerChoiceButtons _playerChoiceButtons;
        private UserInfoView _userInfoView;
        private PlayerHands _playerHands;
        private bool _isSplitActive;

        public PlayerChoiceState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, 
            IUserBalance userBalance)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _userBalance = userBalance;
        } 
        
        public void Enter()
        {
            _isSplitActive = false;
            SetEntityDependencies();
            PreparePlayerButtons(true);
            SubscribeChoiceButtons();
        }

        private void SubscribeChoiceButtons()
        {
            _playerChoiceButtons.OnTakeClick += TakeCard;
            _playerChoiceButtons.OnDoubleClick += Double;
            _playerChoiceButtons.OnSplitClick += Split;
            _playerChoiceButtons.OnStandClick += Stand;
        }

        public void Exit()
        {
            _playerChoiceButtons.OnTakeClick -= TakeCard;
            _playerChoiceButtons.OnDoubleClick -= Double;
            _playerChoiceButtons.OnSplitClick -= Split;
            _playerChoiceButtons.OnStandClick -= Stand;
        }

        private void SetEntityDependencies()
        {
            _cardDispenser = _entityContainer.GetEntity<CardDispenser>();
            _playerChoiceButtons = _entityContainer.GetEntity<PlayerChoiceButtons>();
            _userInfoView = _entityContainer.GetEntity<UserInfoView>();
            _playerHands = _entityContainer.GetEntity<PlayerHands>();
        }

        private async void TakeCard()
        {
            await TakeCardForPlayer();
            CheckFinishTurn();
        }
        
        private async UniTask TakeCardForPlayer()
        {
            await _cardDispenser.HandOutCardToHand(_playerHands.ActiveHand);
            _userInfoView.UpdatePlayerText(_playerHands.ActiveHand.GetValue());
        }

        private async void Split()
        {
            _userBalance.ConfirmSplitBet();
            await _cardDispenser.SplitPlayerCards();
            _userInfoView.UpdatePlayerText(_playerHands.PlayerHand.GetValue());
            PreparePlayerButtons(false);
        }

        private async void Double()
        {
            _userBalance.ConfirmBet(_isSplitActive ? _userBalance.SplitBet : _userBalance.Bet);
            _userInfoView.UpdateBet(_isSplitActive ? _userBalance.SplitBet *= 2 : _userBalance.Bet *= 2);
            _userInfoView.UpdateBalance(_userBalance.Chips);
            await TakeCardForPlayer();
            Stand();
        }

        private void Stand()
        {
            if(_userBalance.SplitBet != 0 && !_isSplitActive) SwitchToSplitTurn();
            else _stateSwitcher.SwitchTo<DealerTurnState>();
        }

        private async void SwitchToSplitTurn()
        {
            _isSplitActive = true;
            _playerHands.SetSplitActiveHand();
            await _cardDispenser.SwapSplitHandToActive();
            _userInfoView.UpdatePlayerText(_playerHands.SplitHand.GetValue());
            PreparePlayerButtons(false);
        }
        
        private void CheckFinishTurn()
        {
            if (_playerHands.ActiveHand.GetValue() >= 21 || _playerHands.ActiveHand.IsBlackJack()) Stand();
            else _playerChoiceButtons.EnableTurn(false, false);
        }

        private void PreparePlayerButtons(bool checkSplit)
        {
            bool isDoublePossible = _userBalance.IsDoublePossible(_userBalance.Bet);
            _playerChoiceButtons.EnableTurn(checkSplit && isDoublePossible && _playerHands.PlayerHand.IsSplitPossible(), isDoublePossible);
        }
    }
}