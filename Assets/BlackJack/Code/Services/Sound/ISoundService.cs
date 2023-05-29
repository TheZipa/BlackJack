using BlackJack.Code.Data.Enums;
using BlackJack.Code.Data.Progress;
using BlackJack.Code.Data.StaticData.Sounds;

namespace BlackJack.Code.Services.Sound
{
    public interface ISoundService
    {
        void Construct(SoundData soundData, Settings userSettings);
        void PlayEffectSound(SoundId soundId);
        void SetBackgroundMusicVolume(float volume);
        void SetEffectsVolume(float volume);
        void PlayBackgroundMusic();
        void StopBackgroundMusic();
        bool MusicMuted { get; set; }
        bool EffectsMuted { get; set; }
    }
}