using BlackJack.Code.Data.Progress;
using BlackJack.Code.Data.Statistic;

namespace BlackJack.Code.Services.PersistentProgress
{
    public interface IPersistentProgress
    {
        PlayerProgress Progress { get; set; }
        PlayerStatisticData CollectPlayerStatisticData();
    }
}