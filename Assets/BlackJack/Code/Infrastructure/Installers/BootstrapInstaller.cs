using BlackJack.Code.Infrastructure.StateMachine.GameStateMachine;
using BlackJack.Code.Infrastructure.StateMachine.States;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.StateFactory;
using Zenject;

namespace BlackJack.Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            RegisterBootstrapInstaller();
            RegisterStateChanger();
            RegisterStateFactory();
            RegisterStateMachine();
        }
         
        public void Initialize() =>
            Container
                .Resolve<IStateSwitcher>()
                .SwitchTo<LoadProgressState>();

        private void RegisterBootstrapInstaller() =>
            Container
                .BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();

        private void RegisterStateMachine() =>
            Container
                .BindInterfacesTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        
        private void RegisterStateChanger() =>
            Container
                .Bind<IStateSwitcher>()
                .To<StateSwitcher>()
                .AsSingle()
                .NonLazy();
        
        private void RegisterStateFactory() =>
            Container
                .Bind<IStateFactory>()
                .To<StatesFactory>()
                .AsSingle()
                .NonLazy();
    }
}