using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.Players;

namespace BlackJack.Code.Services.Factories.GameFactory
{
    public interface IGameFactory
    {
        CardDeck CreateCardDeck();
        PlayerHands CreatePlayerHands();
        CardDispenser CreateCardDispenser();
    }
}