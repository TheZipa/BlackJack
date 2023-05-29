using BlackJack.Code.Core.UI;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Core.UI.Menu;
using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Core.UI.TopPanel;
using BlackJack.Code.Data.StaticData.Configs;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using UnityEngine;

namespace BlackJack.Code.Services.Factories.UIFactory
{ 
    public class UIFactory : IUIFactory
    {
        private readonly ISoundService _soundService;
        private readonly IStaticData _staticData;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;

        public UIFactory(IEntityContainer entityContainer, IPersistentProgress progress, IStaticData staticData,
            IStateSwitcher stateSwitcher, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _progress = progress;
            _soundService = soundService;
            _staticData = staticData;
            _stateSwitcher = stateSwitcher;
        }

        public GameObject CreateRootCanvas() =>
            Object.Instantiate(_staticData.BlackJackPrefabs.RootCanvasPrefab);

        public TopPanelView CreateTopPanel(Transform parent)
        {
            TopPanelView topPanelView = Object.Instantiate(_staticData.BlackJackPrefabs.TopPanelViewPrefab, parent);
            topPanelView.Construct(_soundService, _entityContainer.GetEntity<SettingsView>());

            _entityContainer.RegisterEntity(new TopPanel(topPanelView,
                _entityContainer.GetEntity<StatisticsScreenView>(), _stateSwitcher));
            return topPanelView;
        }

        public BetChipsPanel CreateBetChips(Transform parent)
        {
            BetChipsPanel betChipsPanel = Object.Instantiate(_staticData.BlackJackPrefabs.BetChipsPanelPrefab, parent);
            betChipsPanel.Construct(CreateBetChipViews(_staticData.BlackJackSettingsConfig.BetChipData, betChipsPanel.transform), _soundService);
            betChipsPanel.Disable();
            _entityContainer.RegisterEntity(betChipsPanel);
            return betChipsPanel;
        }
        
        public UserInfoView CreateUserInfoView(Transform parent)
        {
            UserInfoView userInfoView = Object.Instantiate(_staticData.BlackJackPrefabs.UserInfoViewPrefab, parent);
            userInfoView.UpdateBalance(_progress.Progress.Balance);
            _entityContainer.RegisterEntity(userInfoView);
            return userInfoView;
        }

        public PlayerChoiceButtons CreatePlayerChoiceButtons(Transform parent)
        {
            PlayerChoiceButtons playerChoiceButtons =
                Object.Instantiate(_staticData.BlackJackPrefabs.PlayerChoiceButtonsPrefab, parent);
            playerChoiceButtons.Construct(_soundService);
            playerChoiceButtons.DisableAll();
            _entityContainer.RegisterEntity(playerChoiceButtons);
            return playerChoiceButtons;
        }

        public Popup CreateWinPopUp(Transform parent)
        {
            Popup popup = Object.Instantiate(_staticData.BlackJackPrefabs.PopupPrefab, parent);
            popup.Construct(_soundService);
            _entityContainer.RegisterEntity(popup);
            return popup;
        }

        public RegisterScreenView CreateRegisterScreenView(Transform parent)
        {
            RegisterScreenView registerScreenView = Object.Instantiate(_staticData.BlackJackPrefabs.RegisterScreenViewPrefab, parent);
            registerScreenView.Construct(_soundService);
            return registerScreenView;
        }

        public MainMenuView CreateMainMenu(Transform parent)
        {
            MainMenuView menuView = Object.Instantiate(_staticData.BlackJackPrefabs.MainMenuViewPrefab, parent);
            menuView.Construct(_soundService);
            return menuView;
        }

        private BetChipView[] CreateBetChipViews(BetChipData[] betChipData, Transform parent)
        {
            BetChipView[] betChipViews = new BetChipView[betChipData.Length];
            for (int i = 0; i < betChipData.Length; i++)
            {
                BetChipView betChipView =
                    Object.Instantiate(_staticData.BlackJackPrefabs.BetChipViewPrefab, parent);
                betChipView.Construct(betChipData[i]);
                betChipViews[i] = betChipView;
            }
            return betChipViews;
        }
    }
}