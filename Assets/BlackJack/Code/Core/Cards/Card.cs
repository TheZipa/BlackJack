using BlackJack.Code.Data.Enums;

namespace BlackJack.Code.Core.Cards
{
    public class Card
    {
        public CardValue Value { get; }
        public CardView View { get; }
        
        public Card(CardValue value, CardView view)
        {
            Value = value;
            View = view;
        }
    }
}