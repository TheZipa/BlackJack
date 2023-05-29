using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.UserBalance;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class BetState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IUserBalance _userBalance;

        private PlayerChoiceButtons _playerChoiceButtons;
        private UserInfoView _userInfoView;
        private BetChipsPanel _betChipsPanel;

        public BetState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, IUserBalance userBalance)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _userBalance = userBalance;
        }

        public void Enter()
        {
            _userInfoView = _entityContainer.GetEntity<UserInfoView>();
            PrepareChoiceButtons();
            PrepareBetChips();
            ClearBetPanelView();
            TryResetBalance();
        }

        public void Exit()
        {
            _playerChoiceButtons.OnClearClick -= ClearBet;
            _playerChoiceButtons.OnPlayClick -= ConfirmBetAndStartGame;
            _betChipsPanel.OnBetChipClick -= AddBet;
        }

        private void PrepareChoiceButtons()
        {
            _playerChoiceButtons = _entityContainer.GetEntity<PlayerChoiceButtons>();
            _playerChoiceButtons.EnableClearAndPlay();
            _playerChoiceButtons.OnClearClick += ClearBet;
            _playerChoiceButtons.OnPlayClick += ConfirmBetAndStartGame;
        }

        private void ClearBetPanelView()
        {
            SetCachedBet();
            _userInfoView.ResetPlayersText();
        }

        private void TryResetBalance()
        {
            _userBalance.TryReset();
            _userInfoView.UpdateBalance(_userBalance.Chips);
        }

        private void PrepareBetChips()
        {
            _betChipsPanel = _entityContainer.GetEntity<BetChipsPanel>();
            _betChipsPanel.Enable();
            _betChipsPanel.OnBetChipClick += AddBet;
        }

        private void SetCachedBet()
        {
            _userBalance.SplitBet = 0;
            _userInfoView.UpdateBet(_userBalance.Bet = _userBalance.CachedBet > _userBalance.Chips ? 0 : _userBalance.CachedBet);
        }

        private void ClearBet() => _userInfoView.UpdateBet(_userBalance.Bet = _userBalance.CachedBet = 0);

        private void AddBet(int bet)
        {
            int betSum = _userBalance.Bet + bet;
            if (_userBalance.Chips - betSum < 0) return;
            _userInfoView.UpdateBet(_userBalance.Bet = betSum);
        }

        private void DisableBetView()
        {
            _betChipsPanel.Disable();
            _playerChoiceButtons.DisableClearAndPlay();
        }

        private void ConfirmBetAndStartGame()
        {
            if (_userBalance.Bet == 0) return;
            _userBalance.ConfirmBet(_userBalance.CachedBet = _userBalance.Bet);
            DisableBetView();
            _userInfoView.UpdateBalance(_userBalance.Chips);
            _stateSwitcher.SwitchTo<CardHandOutState>();
        }
    }
}