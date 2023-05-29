namespace BlackJack.Code.Services.UserBalance
{
    public interface IUserBalance
    {
        void AddWin(int winAmount);
        void ConfirmBet(int bet); 
        void TryReset();
        int Chips { get; }
        int Bet { get; set; }
        int SplitBet { get; set; }
        int CachedBet { get; set; }
        void ReturnBet();
        void ConfirmSplitBet();
        bool IsDoublePossible(int bet);
    }
}