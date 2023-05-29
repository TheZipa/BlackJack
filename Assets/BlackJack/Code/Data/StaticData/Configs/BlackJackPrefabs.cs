using BlackJack.Code.Core.Cards;
using BlackJack.Code.Core.UI;
using BlackJack.Code.Core.UI.Gameplay;
using BlackJack.Code.Core.UI.Menu;
using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Core.UI.TopPanel;
using UnityEngine;

namespace BlackJack.Code.Data.StaticData.Configs
{
    [CreateAssetMenu(fileName = "BlackJack Prefabs", menuName = "Static Data/BlackJack Prefabs")]
    public class BlackJackPrefabs : ScriptableObject
    { 
        public GameObject RootCanvasPrefab;
        [Header("Persistent")]
        public SettingsView SettingsViewPrefab;
        public StatisticsScreenView StatisticsScreenViewPrefab;
        [Header("Menu")]
        public MainMenuView MainMenuViewPrefab;
        public RegisterScreenView RegisterScreenViewPrefab;
        [Header("Gameplay UI")]
        public TopPanelView TopPanelViewPrefab;
        public UserInfoView UserInfoViewPrefab;
        public PlayerChoiceButtons PlayerChoiceButtonsPrefab;
        public BetChipView BetChipViewPrefab;
        public BetChipsPanel BetChipsPanelPrefab;
        public Popup PopupPrefab;
        [Header("Card View")]
        public CardView CardViewPrefab;
    }
}