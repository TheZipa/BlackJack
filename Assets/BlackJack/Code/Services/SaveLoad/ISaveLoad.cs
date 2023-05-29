using BlackJack.Code.Data.Progress;

namespace BlackJack.Code.Services.SaveLoad
{
    public interface ISaveLoad
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}