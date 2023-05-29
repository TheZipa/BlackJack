using BlackJack.Code.Data.StaticData.Configs;
using BlackJack.Code.Data.StaticData.Location;
using BlackJack.Code.Data.StaticData.Sounds;
using BlackJack.Code.Services.StaticData.StaticDataProvider;

namespace BlackJack.Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public SoundData SoundData { get; private set; }
        public BlackJackSettingsConfig BlackJackSettingsConfig { get; private set; }
        public BlackJackPrefabs BlackJackPrefabs { get; private set; }
        public BlackJackCardConfig BlackJackCardConfig { get; private set; }
        public LocationData LocationData { get; private set; }

        private readonly IStaticDataProvider _staticDataProvider;

        public StaticData(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            BlackJackSettingsConfig = _staticDataProvider.LoadBlackJackSettingsConfig();
            BlackJackPrefabs = _staticDataProvider.LoadBlackJackPrefabs();
            BlackJackCardConfig = _staticDataProvider.LoadBlackJackCardConfig();
            SoundData = _staticDataProvider.LoadSoundData();
            LocationData = _staticDataProvider.LoadLocationData();
        }
    }
}