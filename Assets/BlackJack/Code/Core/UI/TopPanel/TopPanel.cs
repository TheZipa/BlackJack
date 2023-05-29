using System;
using BlackJack.Code.Core.UI.Statistics;
using BlackJack.Code.Infrastructure.StateMachine.States;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;

namespace BlackJack.Code.Core.UI.TopPanel
{
    public class TopPanel : IDisposable
    {
        private readonly TopPanelView _topPanelView;
        private readonly StatisticsScreenView _statisticsView;
        private readonly IStateSwitcher _stateSwitcher;

        public TopPanel(TopPanelView topPanelView, StatisticsScreenView statisticsView, IStateSwitcher stateSwitcher)
        {
            _topPanelView = topPanelView;
            _statisticsView = statisticsView;
            _stateSwitcher = stateSwitcher;

            _topPanelView.OnExitClick += ExitToMainMenu;
        }

        public void Dispose() => _topPanelView.OnExitClick -= ExitToMainMenu;
        private void ExitToMainMenu() => _stateSwitcher.SwitchTo<MenuState>();
    }
}