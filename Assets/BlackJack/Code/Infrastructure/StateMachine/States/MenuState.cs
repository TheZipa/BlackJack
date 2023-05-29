using System;
using BlackJack.Code.Core.UI.Menu;
using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.Factories.UIFactory;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.SaveLoad;
using BlackJack.Code.Services.SceneLoader;
using BlackJack.Code.Services.UserBalance;
using UnityEngine;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IUserBalance _userBalance;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;

        private const string MenuScene = "Menu";

        private MainMenuView _mainMenuView;
        private StatisticsScreenView _statisticsScreen;
        private RegisterScreenView _registerScreen;

        public MenuState(IStateSwitcher stateSwitcher, ISceneLoader sceneLoader, IUIFactory uiFactory, IUserBalance userBalance,
            IEntityContainer entityContainer, IPersistentProgress persistentProgress, ISaveLoad saveLoad)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _userBalance = userBalance;
            _entityContainer = entityContainer;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
        }

        public void Enter()
        {
            _userBalance.CachedBet = 0;
            _sceneLoader.LoadScene(MenuScene, InitializeUIElements);
        }

        public void Exit()
        {
            _mainMenuView.OnPlayButtonClick -= StartGameplay;
            _mainMenuView.OnStatisticButtonClick -= _statisticsScreen.Show;
            if (_registerScreen != null) _registerScreen.OnRegistrationFinish -= TryRegisterUser;
        }

        private void InitializeUIElements()
        {
            Transform rootCanvas = _uiFactory.CreateRootCanvas().transform;
            _statisticsScreen = _entityContainer.GetEntity<StatisticsScreenView>();
            InitializeMainMenuView(rootCanvas);
            if(IsPlayerNotRegistered()) InitializeRegisterScreen(rootCanvas);
        }

        private void InitializeMainMenuView(Transform rootCanvas)
        {
            _mainMenuView = _uiFactory.CreateMainMenu(rootCanvas);
            _mainMenuView.SetSettingsPanel(_entityContainer.GetEntity<SettingsView>());
            _mainMenuView.OnPlayButtonClick += StartGameplay;
            _mainMenuView.OnStatisticButtonClick += _statisticsScreen.Show;
        }

        private void InitializeRegisterScreen(Transform rootCanvas)
        {
            _registerScreen = _uiFactory.CreateRegisterScreenView(rootCanvas);
            _registerScreen.OnRegistrationFinish += TryRegisterUser;
        }

        private void TryRegisterUser(string nickname)
        {
            if (String.IsNullOrWhiteSpace(nickname) || nickname.Length < 3)
            {
                _registerScreen.ShowWrongText();
            }
            else
            {
                _registerScreen.Hide();
                _persistentProgress.Progress.Nickname = nickname;
                _saveLoad.SaveProgress();
            }
        }

        private bool IsPlayerNotRegistered() => String.IsNullOrEmpty(_persistentProgress.Progress.Nickname);
 
        private void StartGameplay() => _stateSwitcher.SwitchTo<LoadGameplayState>();
    }
}