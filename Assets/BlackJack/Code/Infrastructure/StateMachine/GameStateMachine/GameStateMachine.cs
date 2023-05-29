using System;
using System.Collections.Generic;
using BlackJack.Code.Infrastructure.StateMachine.States;
using BlackJack.Code.Infrastructure.StateMachine.StateSwitcher;
using BlackJack.Code.Services.StateFactory;

namespace BlackJack.Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine, IDisposable
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly IStateSwitcher _stateSwitcher;

        private IExitableState _activeState;

        public GameStateMachine(IStateFactory stateFactory, IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadProgressState)] = stateFactory.Create<LoadProgressState>(),
                [typeof(LoadPersistentEntityState)] = stateFactory.Create<LoadPersistentEntityState>(),
                [typeof(MenuState)] = stateFactory.Create<MenuState>(),
                [typeof(LoadGameplayState)] = stateFactory.Create<LoadGameplayState>(),
                [typeof(BetState)] = stateFactory.Create<BetState>(),
                [typeof(CardHandOutState)] = stateFactory.Create<CardHandOutState>(),
                [typeof(PlayerChoiceState)] = stateFactory.Create<PlayerChoiceState>(),
                [typeof(DealerTurnState)] = stateFactory.Create<DealerTurnState>(),
                [typeof(CompareHandsState)] = stateFactory.Create<CompareHandsState>()
            };
            _stateSwitcher.OnStateSwitched += Enter;
            _stateSwitcher.OnStateSwitchedPayloaded += EnterPayload;
        }

        public void Dispose()
        {
            _stateSwitcher.OnStateSwitched -= Enter;
            _stateSwitcher.OnStateSwitchedPayloaded -= EnterPayload;
        }

        private void Enter(Type enterState)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IState state) state.Enter();
        }

        private void EnterPayload(Type enterState, object payload)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IPayloadedState state) state.Enter(payload);
        }

        private IExitableState ChangeState(Type enterState)
        {
            _activeState?.Exit();
            IExitableState exitableState = _states[enterState];
            _activeState = exitableState;
            return exitableState;
        }
        
        ~GameStateMachine() => _activeState.Exit();
    }
}