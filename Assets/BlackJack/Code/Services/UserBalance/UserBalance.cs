using BlackJack.Code.Services.PersistentProgress;
using BlackJack.Code.Services.SaveLoad;
using BlackJack.Code.Services.StaticData;

namespace BlackJack.Code.Services.UserBalance
{
    public class UserBalance : IUserBalance
    {
        public int Chips => _persistentProgress.Progress.Balance;
        public int Bet { get; set; }
        public int SplitBet { get; set; }
        public int CachedBet { get; set; }
        
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoadService;
        private readonly int _startBalance;

        public UserBalance(IPersistentProgress persistentProgress, ISaveLoad saveLoadService, IStaticData staticData)
        {
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
            _startBalance = staticData.BlackJackSettingsConfig.StartBalance;
        }

        public void AddWin(int winAmount)
        {
            _persistentProgress.Progress.Balance += winAmount;
            _saveLoadService.SaveProgress();
        }

        public void ReturnBet()
        {
            _persistentProgress.Progress.Balance += Bet;
            _saveLoadService.SaveProgress();
        }

        public void ConfirmSplitBet()
        {
            SplitBet = Bet;
            ConfirmBet(SplitBet);
        }

        public void ConfirmBet(int bet)
        {
            _persistentProgress.Progress.Balance -= bet;
            _saveLoadService.SaveProgress();
        }

        public bool IsDoublePossible(int bet) => bet * 2 <= Chips;

        public void TryReset()
        {
            if (_persistentProgress.Progress.Balance != 0) return;
            _persistentProgress.Progress.Balance = _startBalance;
            _saveLoadService.SaveProgress();
        }
    }
}