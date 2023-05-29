using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.Factories.PersistentEntityFactory;
using BlackJack.Code.Services.Factories.UIFactory;
using UnityEngine;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class LoadPersistentEntityState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentEntityFactory _persistentEntityFactory;
        private readonly IUIFactory _uiFactory;

        public LoadPersistentEntityState(IStateSwitcher stateSwitcher, 
            IPersistentEntityFactory persistentEntityFactory, IUIFactory uiFactory)
        {
            _stateSwitcher = stateSwitcher;
            _persistentEntityFactory = persistentEntityFactory;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            Transform rootCanvas = CreateRootCanvas().transform;
            CreatePersistentEntities(rootCanvas);
            _stateSwitcher.SwitchTo<MenuState>();
        }

        public void Exit()
        {
        }

        private GameObject CreateRootCanvas()
        {
            Canvas rootCanvas = _uiFactory.CreateRootCanvas().GetComponent<Canvas>();
            rootCanvas.sortingOrder = 10;
            Object.DontDestroyOnLoad(rootCanvas);
            return rootCanvas.gameObject;
        }

        private void CreatePersistentEntities(Transform rootCanvas)
        {
            _persistentEntityFactory.CreateStatisticsScreen(rootCanvas);
            _persistentEntityFactory.CreateSettings(rootCanvas);
        }
    }
}