using System;

namespace BlackJack.Code.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public string Nickname;
        public string Guid;
        public string TournamentUrl;
        public Settings Settings;
        public int Balance;

        public PlayerProgress(int startBalance)
        {
            Guid = System.Guid.NewGuid().ToString();
            Balance = startBalance;
            Settings = new Settings();
        }
    }
}