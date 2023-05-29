using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.SaveLoad;
using BlackJack.Code.Services.Sound;
using BlackJack.Code.Services.StaticData;
using UnityEngine;

namespace BlackJack.Code.Services.Factories.PersistentEntityFactory
{
    public class PersistentEntityFactory : IPersistentEntityFactory
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;
        private readonly ISoundService _soundService;
        private readonly IStaticData _staticData;

        public PersistentEntityFactory(IEntityContainer entityContainer, IStaticData staticData, 
            IPersistentProgress persistentProgress, ISaveLoad saveLoad, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
            _soundService = soundService;
            _staticData = staticData;
        }
        
        public SettingsView CreateSettings(Transform parent)
        {
            SettingsView settingsView = Object.Instantiate(_staticData.BlackJackPrefabs.SettingsViewPrefab, parent);
            settingsView.Construct(_persistentProgress.Progress.Settings, _soundService);
            _entityContainer.RegisterEntity(new SettingsPanel(settingsView, _soundService, _persistentProgress, _saveLoad));
            _entityContainer.RegisterEntity(settingsView);
            return settingsView;
        }

        public StatisticsScreenView CreateStatisticsScreen(Transform parent)
        {
            StatisticsScreenView statisticsScreenView = 
                Object.Instantiate(_staticData.BlackJackPrefabs.StatisticsScreenViewPrefab, parent);
            statisticsScreenView.Construct(_soundService, _persistentProgress.Progress.TournamentUrl);
            _entityContainer.RegisterEntity(statisticsScreenView);
            return statisticsScreenView;
        }
    }
}