using System.Collections.Generic;
using BlackJack.Code.Core.Cards;
using UnityEngine;

namespace BlackJack.Code.Core.Players
{
    public class PlayerHands
    {
        public Hand ActiveHand { get; private set; }
        public Hand DealerHand { get; }
        public Hand PlayerHand { get; }
        public Hand SplitHand { get; }

        private readonly CardDeck _cardDeck;
        private readonly Vector2 _playerHandLocation;
        private readonly Vector2 _splitHandLocation;

        public PlayerHands(Hand dealerHand, Hand playerHand, Hand splitHand, CardDeck cardDeck)
        {
            _cardDeck = cardDeck;
            DealerHand = dealerHand;
            PlayerHand = playerHand;
            SplitHand = splitHand;
            
            _playerHandLocation = PlayerHand.Location;
            _splitHandLocation = SplitHand.Location;
        }

        public void SplitPlayer()
        {
            SplitHand.TakeCard(PlayerHand.Cards[1]);
            PlayerHand.Cards.RemoveAt(1);
        }

        public void SetDefaultActiveHand()
        {
            PlayerHand.Location = _playerHandLocation;
            SplitHand.Location = _splitHandLocation;
            ActiveHand = PlayerHand;
        }

        public void SetSplitActiveHand()
        {
            PlayerHand.Location = _splitHandLocation;
            SplitHand.Location = _playerHandLocation;
            ActiveHand = SplitHand;
        }

        public void ReturnAllCards()
        {
            ReturnCardsFromHand(DealerHand.Cards);
            ReturnCardsFromHand(PlayerHand.Cards);
            ReturnCardsFromHand(SplitHand.Cards);
        }

        public bool IsPlayerSplit() => SplitHand.Cards.Count > 0;

        private void ReturnCardsFromHand(List<Card> hand)
        {
            _cardDeck.ReturnCards(hand);
            hand.Clear();
        }
    }
}