using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.Factories.GameFactory;
using BlackJack.Code.Services.Factories.UIFactory;
using BlackJack.Code.Services.SceneLoader;
using UnityEngine;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class LoadGameplayState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoader _sceneLoader;

        private const string GameScene = "Game";

        public LoadGameplayState(IStateSwitcher stateSwitcher, IUIFactory uiFactory, 
            IGameFactory gameFactory, ISceneLoader sceneLoader)
        {
            _stateSwitcher = stateSwitcher;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.LoadScene(GameScene, CreateGame);
        }

        public void Exit()
        {
        }

        private void CreateGame()
        {
            CreateGameplayComponents();
            CreateUI();
            _stateSwitcher.SwitchTo<BetState>();
        }

        private void CreateGameplayComponents()
        {
            _gameFactory.CreateCardDeck();
            _gameFactory.CreatePlayerHands();
            _gameFactory.CreateCardDispenser();
        }
 
        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas().transform;
            _uiFactory.CreateTopPanel(root);
            _uiFactory.CreatePlayerChoiceButtons(root);
            _uiFactory.CreateUserInfoView(root);
            _uiFactory.CreateBetChips(root);
            _uiFactory.CreateWinPopUp(root);
        }
    }
}