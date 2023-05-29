using BlackJack.Code.Data.StaticData.Configs;
using BlackJack.Code.Data.StaticData.Location;
using BlackJack.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace BlackJack.Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string BlackJackSettingsConfigPath = "StaticData/BlackJack Settings Config";
        private const string BlackJackCardConfigPath = "StaticData/Card Config";
        private const string BlackJackPrefabsPath = "StaticData/BlackJack Prefabs";
        private const string SoundDataPath = "StaticData/Sound Data";
        private const string LocationDataPath = "StaticData/Location Data";

        public BlackJackSettingsConfig LoadBlackJackSettingsConfig() =>
            Resources.Load<BlackJackSettingsConfig>(BlackJackSettingsConfigPath);

        public BlackJackCardConfig LoadBlackJackCardConfig() =>
            Resources.Load<BlackJackCardConfig>(BlackJackCardConfigPath);

        public BlackJackPrefabs LoadBlackJackPrefabs() =>
            Resources.Load<BlackJackPrefabs>(BlackJackPrefabsPath);

        public SoundData LoadSoundData() =>
            Resources.Load<SoundData>(SoundDataPath);
        
        public LocationData LoadLocationData() =>
            Resources.Load<LocationData>(LocationDataPath);
    }
}