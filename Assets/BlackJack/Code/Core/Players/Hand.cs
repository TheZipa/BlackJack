using System.Collections.Generic;
using BlackJack.Code.Core.Cards;
using BlackJack.Code.Data.Enums;
using UnityEngine;

namespace BlackJack.Code.Core.Players
{
    public class Hand
    {
        public int LastCardIndex => Cards.Count - 1;
        public Vector2 Location { get; set; }
        public List<Card> Cards { get; } = new List<Card>(5);

        public Hand(Vector2 location) => Location = location;

        public void TakeCard(Card card)
        {
            Cards.Add(card);
            card.View.SetLayer(Cards.Count);
        }

        public int GetFirstCardValue() => Cards[0].Value == CardValue.Ace ? 11 : (int)Cards[0].Value;

        public int GetValue()
        {
            int handValue = 0;
            int aceCount = 0;
            foreach (Card card in Cards)
            {
                if (card.Value == CardValue.Ace) aceCount++;
                else handValue += (int) card.Value;
            }

            if (aceCount == 0) return handValue;
            int aceHardValue = aceCount * 11;
            if (aceHardValue + handValue > 21) return handValue + aceCount;
            return handValue + aceHardValue;
        }

        public bool IsBlackJack() => Cards.Count == 2 && GetValue() == 21;

        public bool IsSplitPossible() => Cards[0].Value == Cards[1].Value;
    }
}