using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.Players;
using BlackJack.Code.Data.StaticData;
using BlackJack.Code.Data.StaticData.Location;
using BlackJack.Code.Services.CardMove;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using Object = UnityEngine.Object;

namespace BlackJack.Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly ISoundService _soundService;
        private readonly ICardMover _cardMover;
 
        public GameFactory(IStaticData staticData, IEntityContainer entityContainer, 
            ISoundService soundService, ICardMover cardMover)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _soundService = soundService;
            _cardMover = cardMover;
        }

        public CardDeck CreateCardDeck()
        {
            CardDeck cardDeck = new CardDeck(CreateCards(), _staticData.LocationData.CardDeckLocation.Position, 
                _staticData.BlackJackPrefabs.CardViewPrefab.transform.localScale);
            _entityContainer.RegisterEntity(cardDeck);
            return cardDeck;
        }
 
        public PlayerHands CreatePlayerHands()
        {
            Hand dealerHand = new Hand(_staticData.LocationData.DealerLocation.Position);
            Hand playerHand = new Hand(_staticData.LocationData.PlayerLocation.Position);
            Hand playerSplitHand = new Hand(_staticData.LocationData.SplitLocation.Position);
            PlayerHands playerHands = new PlayerHands(dealerHand, playerHand, playerSplitHand,
                _entityContainer.GetEntity<CardDeck>());
            _entityContainer.RegisterEntity(playerHands);
            return playerHands;
        }

        public CardDispenser CreateCardDispenser()
        {
            CardDispenser cardDispenser = new CardDispenser(_cardMover, _entityContainer.GetEntity<CardDeck>(),
                _entityContainer.GetEntity<PlayerHands>());
            _entityContainer.RegisterEntity(cardDispenser);
            return cardDispenser;
        }

        private Card[] CreateCards()
        {
            CardData[] data = _staticData.BlackJackCardConfig.CardData;
            Card[] cards = new Card[data.Length];
            
            for (int i = 0; i < data.Length; i++) 
                cards[i] = CreateCard(data[i], _staticData.LocationData.CardDeckLocation);

            return cards;
        }

        private Card CreateCard(CardData cardData, Location location)
        {
            CardView cardView = Object.Instantiate(_staticData.BlackJackPrefabs.CardViewPrefab, 
                location.Position, location.Rotation);
            cardView.Construct(cardData.CardSprite, _staticData.BlackJackCardConfig.Shirt);
            cardView.Disable();
            return new Card(cardData.CardValue, cardView);
        }
    }
}