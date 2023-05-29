using BlackJack.Code.Data.StaticData.Configs;
using BlackJack.Code.Data.StaticData.Location;
using BlackJack.Code.Data.StaticData.Sounds;

namespace BlackJack.Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        BlackJackSettingsConfig LoadBlackJackSettingsConfig();
        BlackJackPrefabs LoadBlackJackPrefabs();
        SoundData LoadSoundData();
        LocationData LoadLocationData();
        BlackJackCardConfig LoadBlackJackCardConfig();
    }
}