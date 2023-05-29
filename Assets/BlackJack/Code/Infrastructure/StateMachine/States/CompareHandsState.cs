using System;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Core.UI;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Data.Enums;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.UserBalance;
using BlackJack.Code.Services.WebRequest;
using Newtonsoft.Json;
using UnityEngine;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class CompareHandsState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IUserBalance _userBalance;
        private readonly IWebRequestService _webRequestService;
        private readonly IPersistentProgress _persistentProgress;
        private HandResult _playerHandResult;
        private HandResult _splitHandResult;

        private Popup _popup;
        private PlayerHands _playerHands;

        public CompareHandsState(IStateSwitcher stateSwitcher, IEntityContainer entityContainer, IUserBalance userBalance,
            IWebRequestService webRequestService, IPersistentProgress persistentProgress)
        {
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
            _userBalance = userBalance;
            _webRequestService = webRequestService;
            _persistentProgress = persistentProgress;
        }

        public void Enter()
        {
            _playerHandResult = _splitHandResult = new HandResult();
            SetEntityDependencies();
            _popup.OnClosed += StartNewRound;

            StartCompare();
            _entityContainer.GetEntity<UserInfoView>().UpdateBalance(_userBalance.Chips);
        }

        public void Exit() => _popup.OnClosed -= StartNewRound;

        private void StartCompare()
        {
            int dealerHandValue = _playerHands.DealerHand.GetValue();
            bool isDealerBlackjack = _playerHands.DealerHand.IsBlackJack();
            ComparePlayerAndDealerHands( ref _playerHandResult, _playerHands.PlayerHand.GetValue(), dealerHandValue, 
                 _userBalance.Bet, _playerHands.PlayerHand.IsBlackJack(), isDealerBlackjack);

            if (_playerHands.IsPlayerSplit())
            {
                ComparePlayerAndDealerHands(ref _splitHandResult, _playerHands.SplitHand.GetValue(), dealerHandValue,
                    _userBalance.SplitBet, _playerHands.SplitHand.IsBlackJack(), isDealerBlackjack);
                _popup.ShowSplitResult(_playerHandResult, _splitHandResult);
            }
            else
            {
                _popup.ShowSingleResult(_playerHandResult);
            }

            SendUserStatistic();
        }

        private void SendUserStatistic()
        {
            string tournamentUrl = _persistentProgress.Progress.TournamentUrl;
            if (String.IsNullOrEmpty(tournamentUrl)) return;
            string playerStatisticJson = JsonUtility.ToJson(_persistentProgress.CollectPlayerStatisticData());
            _webRequestService.POST(tournamentUrl, playerStatisticJson, OnUserStatisticSent);
        }

        private void OnUserStatisticSent(string result) {}

        private void ComparePlayerAndDealerHands(ref HandResult handResult, int playerHandValue, int dealerHandValue,
            int bet, bool isPlayerBlackjack, bool isDealerBlackjack)
        {
            if (playerHandValue <= 21 && dealerHandValue > 21) 
                PayWinForPlayer(ref handResult, bet, isPlayerBlackjack);
            else if (isDealerBlackjack && !isPlayerBlackjack || playerHandValue > 21 || playerHandValue < dealerHandValue) 
                handResult.DefineResult(GameResultType.lose);
            else if (playerHandValue == dealerHandValue) DefineStay(ref handResult);
            else PayWinForPlayer(ref handResult, bet, isPlayerBlackjack);
        }
        
        private void PayWinForPlayer(ref HandResult handResult, int bet, bool isBlackjack)
        {
            int winAmount = bet * (isBlackjack ? 3 : 2);
            _userBalance.AddWin(winAmount);
            handResult.DefineResult(GameResultType.win, winAmount);
        }

        private void DefineStay(ref HandResult handResult)
        {
            _userBalance.ReturnBet();
            handResult.DefineResult(GameResultType.stay);
        }

        private void StartNewRound()
        {
            _playerHands.ReturnAllCards();
            _stateSwitcher.SwitchTo<BetState>();
        }

        private void SetEntityDependencies()
        {
            _playerHands = _entityContainer.GetEntity<PlayerHands>();
            _popup = _entityContainer.GetEntity<Popup>();
        }
    }
}