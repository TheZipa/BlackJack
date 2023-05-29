using System.Collections.Generic;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Services.CardMove;
using Cysharp.Threading.Tasks;

namespace BlackJack.Code.Core.Cards
{
    public class CardDispenser
    {
        private readonly List<UniTask> _swapTasks = new List<UniTask>(8);
        private readonly ICardMover _cardMover;
        private readonly CardDeck _cardDeck;
        private readonly PlayerHands _playerHands;

        public CardDispenser(ICardMover cardMover, CardDeck cardDeck, PlayerHands playerHands)
        {
            _cardMover = cardMover;
            _cardDeck = cardDeck;
            _playerHands = playerHands;
        }

        public async UniTask DispenseTwoCardsForDealer()
        {
            await HandOutCardToHand(_playerHands.DealerHand);
            await HandOutCardToHand(_playerHands.DealerHand, true);
        }

        public async UniTask DispenseTwoCardsForPlayer()
        {
            for (int i = 0; i < 2; i++) 
                await HandOutCardToHand(_playerHands.PlayerHand);
        }

        public async UniTask SplitPlayerCards()
        {
            _playerHands.SplitPlayer();
            await _cardMover.MoveCardToSplit(_playerHands.SplitHand.Cards[0].View, _playerHands.SplitHand.Location, 0);
            await HandOutCardToHand(_playerHands.PlayerHand);
        }

        public async UniTask SwapSplitHandToActive()
        {
            await UniTask.WhenAny(GetHandsSwapTasks());
            await HandOutCardToHand(_playerHands.SplitHand);
        }
        
        private UniTask[] GetHandsSwapTasks()
        {
            _swapTasks.Clear();
            for (var i = 0; i < _playerHands.PlayerHand.Cards.Count; i++)
                _swapTasks.Add(_cardMover.MoveCardToSplit(_playerHands.PlayerHand.Cards[i].View,
                    _playerHands.PlayerHand.Location, i));

            _swapTasks.Add(_cardMover.MoveCardFromSplit(_playerHands.SplitHand.Cards[0].View, _playerHands.ActiveHand.Location, 0));
            return _swapTasks.ToArray();
        }

        public async UniTask HandOutCardToHand(Hand hand, bool isShirtUp = false)
        {
            Card card = _cardDeck.GetCard(isShirtUp);
            hand.TakeCard(card);
            await _cardMover.MoveCardWithOffset(card.View, hand.Location, hand.LastCardIndex);
        }
    }
}