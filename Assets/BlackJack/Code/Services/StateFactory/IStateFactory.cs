using BlackJack.Code.Infrastructure.StateMachine.States;

namespace BlackJack.Code.Services.StateFactory
{
    public interface IStateFactory
    {
        T Create<T>() where T : IExitableState;
    }
}