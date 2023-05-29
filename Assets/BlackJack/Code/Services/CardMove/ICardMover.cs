using BlackJack.Code.Core.Cards;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BlackJack.Code.Services.CardMove
{
    public interface ICardMover
    {
        UniTask MoveCardToSplit(CardView cardView, Vector2 position, int offsetIndex);
        UniTask MoveCardWithOffset(CardView cardView, Vector2 position, int offsetIndex);
        UniTask RotateCard (CardView cardView, bool isShirtUp);
        UniTask MoveCardFromSplit(CardView cardView, Vector2 position, int offsetIndex);
    }
}