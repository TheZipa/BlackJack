using System.Collections.Generic;
using BlackJack.Code.Extensions;
using UnityEngine;

namespace BlackJack.Code.Core.Cards
{
    public class CardDeck
    {
        private readonly Stack<Card> _cards;
        private readonly Vector2 _location;
        private readonly Vector2 _defaultScale;

        public CardDeck(Card[] cards, Vector2 position, Vector2 defaultScale)
        {
            _cards = new Stack<Card>(cards);
            _location = position;
            _defaultScale = defaultScale;
        }

        public void Shuffle() => _cards.Shuffle();
        
        public Card GetCard(bool isShirtUp = false)
        {
            Card topCard = _cards.Pop();
            topCard.View.Enable();
            if(isShirtUp) topCard.View.SetShirtSprite();
            else topCard.View.SetValueSprite();
            return topCard;
        }

        public void ReturnCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                ResetCardView(card.View);
                _cards.Push(card);
            }
        }

        private void ResetCardView(CardView card)
        {
            card.Disable();
            card.FadeColor(Color.white, 0f);
            card.transform.position = _location;
            card.transform.localScale = _defaultScale;
        }
    }
}