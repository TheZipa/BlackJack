using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.EntityContainer;
using BlackJack.Code.Services.Factories.UIFactory;
using BlackJack.Code.Services.PersistentProgress;

namespace BlackJack.Code.Infrastructure.StateMachine.States
{
    public class RegisterState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentProgress _persistentProgress;
        private readonly IEntityContainer _entityContainer;

        public RegisterState(IStateSwitcher stateSwitcher, IPersistentProgress persistentProgress, IUIFactory uiFactory)
        {
            _stateSwitcher = stateSwitcher;
            _persistentProgress = persistentProgress;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}