using BlackJack.Code.Core.UI.Settings;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Data.Progress;
using UnityEngine;

namespace BlackJack.Code.Services.Factories.PersistentEntityFactory
{
    public interface IPersistentEntityFactory
    {
        SettingsView CreateSettings(Transform parent);
        StatisticsScreenView CreateStatisticsScreen(Transform parent);
    }
}