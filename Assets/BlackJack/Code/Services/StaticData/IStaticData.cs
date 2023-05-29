using BlackJack.Code.Data.StaticData.Configs;
using BlackJack.Code.Data.StaticData.Location;
using BlackJack.Code.Data.StaticData.Sounds;

namespace BlackJack.Code.Services.StaticData
{
    public interface IStaticData
    {
        SoundData SoundData { get; }
        LocationData LocationData { get; }
        BlackJackSettingsConfig BlackJackSettingsConfig { get; }
        BlackJackPrefabs BlackJackPrefabs { get; }
        BlackJackCardConfig BlackJackCardConfig { get; }
    }
}