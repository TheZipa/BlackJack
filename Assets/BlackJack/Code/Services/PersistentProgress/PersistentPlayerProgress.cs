using System;
using BlackJack.Code.Data.Progress;
using BlackJack.Code.Data.Statistic;
using UnityEngine;

namespace BlackJack.Code.Services.PersistentProgress
{
    public class PersistentPlayerProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }

        public PlayerStatisticData CollectPlayerStatisticData() => new PlayerStatisticData()
            {
                appbundle = Application.identifier,
                guid = Progress.Guid,
                name = Progress.Nickname,
                score = Progress.Balance,
                dateandtime = DateTime.Today.ToShortDateString()
            };
    }
}